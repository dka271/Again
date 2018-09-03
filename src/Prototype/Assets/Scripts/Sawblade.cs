using UnityEngine;
using System.Collections;

public class Sawblade : Obstacle {
	
	public float speed = 300;
	Material bloody;

	void Start() {
		GetComponent<Renderer>().enabled = false;
	}

	void Update () {
		transform.Rotate(Vector3.forward * -speed * Time.deltaTime,Space.World);
	}
	
	
	void OnTriggerEnter(Collider c) {
		if (c.tag == "Player") {
			c.GetComponent<Entity>().bloodParticles.emissionRate = 2500;
			c.GetComponent<Entity>().bloodParticles.startSpeed = 6;
			c.GetComponent<Entity>().TakeDamage(10);
		}
	}

	void OnParticleCollision(GameObject particleSystem) {
		GetComponent<Renderer>().enabled = true;
	}
}
