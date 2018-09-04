using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
    public int currentLevel;
	private GameObject currentPlayer;
	private GameCamera cam;
	private Vector3 checkpoint;

	public static int levelCount = 10;
    private static int currNumDeathsThisLevel = 0;
    private static long timeToCompleteCurrLevel = 999999;
    private double Timer = 0.0;

    private bool loading;

	void Start () {
		cam = GetComponent<GameCamera>();

		if (GameObject.FindGameObjectWithTag("Spawn")) {
			checkpoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
		}

		SpawnPlayer(checkpoint);
	}
	
	// Spawn player
	private void SpawnPlayer(Vector3 spawnPos) {
		currentPlayer = Instantiate(player,spawnPos,Quaternion.identity) as GameObject;
		cam.SetTarget(currentPlayer.transform);
	}

	private void Update() {
        Timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.
        //This will show the timer on screen
        // guiText.text = "" + Timer;
        if (!currentPlayer) {
			if (Input.GetButtonDown("Respawn")) {
                currNumDeathsThisLevel++;
                GameObject body = GameObject.FindGameObjectWithTag("DeadBody");
				if (body)
				{
					Destroy(body);
				}
				SpawnPlayer(checkpoint);
			}
		}
		if (Input.GetButtonDown("Escape")) {
			//currentLevel = 1;
            SceneManager.LoadScene("Main Menu");

        }
	}

	public void SetCheckpoint(Vector3 cp) {
		checkpoint = cp;
	}

	public void EndLevel() {

        if (currentLevel < levelCount && !loading) {
			loading = true;

            //This unlocks the next level
            PlayerPrefs.SetInt("Level " + (currentLevel+1), 1);
            Console.Write("thingy: " + PlayerPrefs.GetInt("Level " + (currentLevel + 1)));

            //Sets the fastest completion time
            if (PlayerPrefs.GetInt("Level " + currentLevel + "_timegoal") != 0 && PlayerPrefs.GetInt("Level " + currentLevel + "_timegoal") > Timer)
            {
                PlayerPrefs.SetInt("Level " + currentLevel + "_timegoal", Convert.ToInt32(Timer));
            }

            //Sets whether or not this level was completed
            PlayerPrefs.SetInt("Level " + currentLevel + "_completed", 1);
            
            //Sets if the level has been completed without dying or not
            if (currNumDeathsThisLevel == 0) {
                PlayerPrefs.SetInt("Level " + currentLevel + "_deathless", 1);
            }
            
            //currentLevel++;
            currNumDeathsThisLevel = 0;
            timeToCompleteCurrLevel = 999999;
            SceneManager.LoadScene("LevelSelectMenu");

            loading = false;
		}
		else {
            currNumDeathsThisLevel = 0;
            timeToCompleteCurrLevel = 999999;
            SceneManager.LoadScene("Credits");
		}
	}
}
