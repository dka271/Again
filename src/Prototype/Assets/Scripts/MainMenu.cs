using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class MainMenu : MonoBehaviour {

	public GUISkin AgainGUISkin;
	public Vector2 buttonStartLocation;
	public Vector2 buttonHelpLocation;
	public Vector2 buttonCreditsLocation;
	public Vector2 buttonExitLocation;
	public _GUIClasses.Location center = new _GUIClasses.Location();
	

	// Update is called once per frame
	void Update () {

		center.updateLocation();
	
	}

	void OnGUI()
	{
		GUI.skin = AgainGUISkin;

		if(GUI.Button(new Rect (center.offset.x + buttonStartLocation.x, center.offset.y + buttonStartLocation.y, 200, 50), "AGAIN!!"))
		{
			Application.LoadLevel("Obj Menu");
		}

		if(GUI.Button(new Rect (center.offset.x + buttonHelpLocation.x, center.offset.y + buttonHelpLocation.y, 200, 50), "HELP ME!"))
		{
			Application.LoadLevel("Help Menu2");
		}

		if(GUI.Button(new Rect (center.offset.x + buttonCreditsLocation.x, center.offset.y + buttonCreditsLocation.y, 200, 50), "WHO DID THIS?"))
		{
			Application.LoadLevel("Credits");
		}

		if(GUI.Button(new Rect (center.offset.x + buttonExitLocation.x, center.offset.y + buttonExitLocation.y, 200, 50), "NO MORE!"))
		{
			Application.Quit();
		}
	}
}
