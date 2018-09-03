using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
	}
	
	void OnParticleCollision(GameObject particleSystem) {
		GetComponent<Renderer>().enabled = true;
	}
}
