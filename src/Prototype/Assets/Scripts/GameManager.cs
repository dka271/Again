using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	private GameObject currentPlayer;
	private GameCamera cam;
	private Vector3 checkpoint;

	public static int levelCount = 10;
	public static int currentLevel = 1;

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
		if (!currentPlayer) {
			if (Input.GetButtonDown("Respawn")) {
				GameObject body = GameObject.FindGameObjectWithTag("DeadBody");
				if (body)
				{
					Destroy(body);
				}
				SpawnPlayer(checkpoint);
			}
		}
		if (Input.GetButtonDown("Escape")) {
			currentLevel = 1;
			Application.LoadLevel("Main Menu");
		}
	}

	public void SetCheckpoint(Vector3 cp) {
		checkpoint = cp;
	}

	public void EndLevel() {

		if (currentLevel < levelCount && !loading) {
			loading = true;
			currentLevel++;
			Application.LoadLevel("Level " + currentLevel);
			loading = false;
		}
		else {
			Application.LoadLevel("Credits");
		}
	}
}
