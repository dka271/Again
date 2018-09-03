using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {

	public ParticleSystem blood;
	Transform brick;

	// Use this for initialization
	void Start () {
		blood = GetComponent<ParticleSystem> ();
		//blood.Play ();
		}
	// Update is called once per frame
	void Update () {
		//this.Bleed ();
	}
	//public void Bleed() {
		//if (player.isDead () == true) {
						//blood.Play ();
				//}
	//}


}
