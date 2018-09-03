using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	private static BGM instance = null;
	public static BGM Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	
	// any other methods you need
}
