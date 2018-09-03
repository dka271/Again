using UnityEngine;
using System.Collections;

public class Spike : Obstacle {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider c) {
				if (c.tag == "Player") {
					c.GetComponent<Entity>().bloodParticles.emissionRate = 600;
					c.GetComponent<Entity>().bloodParticles.startSpeed = 4;
					c.GetComponent<Entity>().TakeDamage (10);
				}
		}
	void OnParticleCollision(GameObject particleSystem) {
		GetComponent<Renderer>().enabled = true;
		}
}
