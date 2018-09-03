using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : Entity {
	
	// Player Handling
	public float regularGravity = 20;
	public float diveGravity = 100;
	public float currentGravity;
	public float walkSpeed = 8;
	//public float runSpeed = 12;
	public float acceleration = 30;
	public float jumpHeight = 12;
	public float slideDeceleration = 8;
	
	private float initiateSlideThreshold = 7;
	
	// System
	private float animationSpeed;
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	private float moveDirX;
	
	// States
	private bool jumping;
	private bool sliding;
	private bool diving;
	private bool wallHolding;
	private bool stopSliding;
	
	
	// Components
	private PlayerPhysics playerPhysics;
	private Animator animator;
	private GameManager manager;
	
	
	void Start () {
		currentGravity = regularGravity;
		playerPhysics = GetComponent<PlayerPhysics>();
		animator = GetComponent<Animator>();
		manager = Camera.main.GetComponent<GameManager>();
		//animator.SetLayerWeight(1,1);
	}
	
	void Update () {
		
		//Check for end of dive
		if (diving && playerPhysics.grounded) {

			// Minimum velocity to suicide
			if (amountToMove.y <= -25.0f)
			{
				Instantiate(deadBody, transform.position, transform.rotation);
				bloodParticles.emissionRate = 200 + (int)(amountToMove.magnitude * 10);
				bloodParticles.startSpeed = 3;
				TakeDamage(health);
			}
			else
			{
				diving = false;
				animator.SetBool("Diving",false);
				currentGravity = regularGravity;
			}
		}
		
		// Reset acceleration upon collision
		if (playerPhysics.movementStopped) {
			targetSpeed = 0;
			currentSpeed = 0;
		}
		
		
		// If player is touching the ground
		if (playerPhysics.grounded) {
			amountToMove.y = 0;
			
			if (wallHolding) {
				wallHolding = false;
				animator.SetBool("Wall Hold", false);
			}
			
			// Jump logic
			if (jumping) {
				jumping = false;
				animator.SetBool("Jumping",false);
			}
			
			// Slide logic
			if (sliding) {
				if (Mathf.Abs(currentSpeed) < .25f || stopSliding || Input.GetButtonUp("Slide/DiveBomb")) {
					stopSliding = false;
					sliding = false;
					animator.SetBool("Sliding",false);
					//playerPhysics.ResetCollider();
					playerPhysics.standUp();
				}
			}
		}
		else {
			if (!wallHolding) {
				if (playerPhysics.canWallHold) {
					wallHolding = true;
					animator.SetBool("Wall Hold", true);
				}
			}
		}
		
		// Slide Input
		if (Input.GetButtonDown("Slide/DiveBomb")) {
			if(playerPhysics.grounded)
			{
				//Do Slide
				if (Mathf.Abs(currentSpeed) > initiateSlideThreshold) {
					sliding = true;
					animator.SetBool("Sliding", true);
					targetSpeed = 0;
					
					playerPhysics.SetCollider(new Vector3(5.46f,3.7f,3), new Vector3(0.0f,0.0f,0));
				}
			}
			else
			{
				//Do DiveBomb
				currentGravity = diveGravity;
				animator.SetBool("Diving", true);
				diving = true;
			}
		}
		
		// Jump Input
		if (Input.GetButtonDown("Jump")) {
			if (sliding) {
				stopSliding = true;
			}
			else if (playerPhysics.grounded || wallHolding) {
				amountToMove.y = jumpHeight;
				jumping = true;
				animator.SetBool("Jumping", true);
				
				if (wallHolding) {
					wallHolding = false;
					animator.SetBool("Wall Hold", false);
				}
			}
		}
		
		
		// Set animator parameters
		animationSpeed = IncrementTowards(animationSpeed,Mathf.Abs(targetSpeed),acceleration);
		animator.SetFloat("Speed",animationSpeed);
		
		// Input
		moveDirX = Input.GetAxisRaw("Horizontal");
		if (!sliding) {
			float speed = walkSpeed;
			targetSpeed = moveDirX * speed;
			currentSpeed = IncrementTowards(currentSpeed, targetSpeed,acceleration);
			
			// Face Direction
			if (moveDirX !=0 && !wallHolding) {
				transform.eulerAngles = (moveDirX<0)?Vector3.up * 180:Vector3.zero;
			}
		}
		else {
			currentSpeed = IncrementTowards(currentSpeed, targetSpeed,slideDeceleration);
		}
		
		// Set amount to move
		amountToMove.x = currentSpeed;
		
		if (wallHolding) {
			amountToMove.x = 0;
			if (Input.GetAxisRaw("Vertical") != -1) {
				amountToMove.y = 0;	
			}
		}
		
		amountToMove.y -= currentGravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime, moveDirX);
		
	}
	
	void OnTriggerEnter(Collider c) {
		if (c.tag == "Checkpoint") {
			manager.SetCheckpoint(c.transform.position);
		}
		if (c.tag == "Finish") {
			manager.EndLevel();
		}
	}
	
	// Increase n towards target by speed
	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}
}
