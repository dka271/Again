using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	
	public float health;
	public GameObject ragdoll;
	public bool dead;
	//public Blood bloodScript;
	//public ParticleSystem blood;
	//public GameObject blood;
	public ParticleSystem bloodParticles;
	public GameObject deadBody;
	
	public void TakeDamage(float dmg) {
		health -= dmg;
		
		if (health <= 0) {
			Die();	
		}
	}
	
	public void Die() {
		Destroy(this.gameObject);
		dead = true;

		// Make sure this is at least gory enough to be worth their time.
		if (bloodParticles.emissionRate < 200) {
			bloodParticles.emissionRate = 200;
		}

		Instantiate(bloodParticles, transform.position, transform.rotation);
	}

	public bool isDead() {
		return dead;
	}
}
