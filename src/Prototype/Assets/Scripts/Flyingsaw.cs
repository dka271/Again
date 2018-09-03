using UnityEngine;
using System.Collections;

public class Flyingsaw : MonoBehaviour {

	public float speed = 300;
	Material bloody;
	
	void Start() {
		GetComponent<Renderer>().enabled = false;
		transform.position = new Vector2 (-20, 10);
	}
	
	void Update () {
		transform.Rotate(Vector3.forward * -speed * Time.deltaTime,Space.World);
		transform.position = new Vector3(Mathf.PingPong(Time.time * 25, 50) , transform.position.y,  transform.position.z);
	}
	
	
	void OnTriggerEnter(Collider c) {
		if (c.tag == "Player") {
			c.GetComponent<Entity>().TakeDamage(10);
		}
	}
	
	void OnParticleCollision(GameObject particleSystem) {
		GetComponent<Renderer>().enabled = true;
	}


}
