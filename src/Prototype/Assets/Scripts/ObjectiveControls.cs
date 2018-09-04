using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class ObjectiveControls : MonoBehaviour {

	public GUISkin AgainGUISkin;
	public Vector2 buttonMenuLocation;
	public _GUIClasses.Location center = new _GUIClasses.Location();
	

	// Update is called once per frame
	void Update () {

		center.updateLocation();
	
	}

	void OnGUI()
	{
		GUI.skin = AgainGUISkin;

		if(GUI.Button(new Rect (center.offset.x + buttonMenuLocation.x, center.offset.y + buttonMenuLocation.y, 140, 50), "BEGIN!"))
		{
			//GameManager.currentLevel = 1;
            SceneManager.LoadScene("LevelSelectMenu");
        }

	}
}
