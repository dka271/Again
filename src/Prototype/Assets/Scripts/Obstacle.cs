using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public Color trueColor;
	private bool isVisible;

	public void ShowTrueColor() {
		if (!isVisible) {
			this.GetComponent<Renderer>().material.color = trueColor;
			isVisible = true;
		}
	}
}
