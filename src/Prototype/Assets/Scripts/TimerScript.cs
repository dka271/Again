using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public float currentTimeInLevel;
    public Text timerText;
    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        currentTimeInLevel = 0;
    }
	
	// Update is called once per frame
	void Update () {
        currentTimeInLevel = Time.time - startTime;

        string minutes = ((int)currentTimeInLevel / 60).ToString();
        string seconds = (currentTimeInLevel % 60).ToString("f2");

        if (minutes == "0")
        {
            timerText.text = seconds;
        }
        else
        {
            timerText.text = minutes + ":" + seconds;
        }
	}
}
