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
    private TimerScript timer;

    private bool loading;

	void Start () {
		cam = GetComponent<GameCamera>();

		if (GameObject.FindGameObjectWithTag("Spawn")) {
			checkpoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
		}

		SpawnPlayer(checkpoint);
        timer = GameObject.Find("TimerText").GetComponent<TimerScript>();
    }
	
	// Spawn player
	private void SpawnPlayer(Vector3 spawnPos) {
		currentPlayer = Instantiate(player,spawnPos,Quaternion.identity) as GameObject;
		cam.SetTarget(currentPlayer.transform);
	}

	private void Update() {
        
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
            Debug.Log("first" + PlayerPrefs.GetFloat("Level " + currentLevel + "_timegoal"));
            Debug.Log("second" + timer.currentTimeInLevel);
            if (PlayerPrefs.GetFloat("Level " + currentLevel + "_timegoal") == 0 || PlayerPrefs.GetFloat("Level " + currentLevel + "_timegoal") > timer.currentTimeInLevel)
            {
                PlayerPrefs.SetFloat("Level " + currentLevel + "_timegoal", timer.currentTimeInLevel);
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
