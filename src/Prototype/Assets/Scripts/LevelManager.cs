using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [System.Serializable]
    public class Level
    {
        public string LevelText;
        public int Unlocked; //0 = locked, 1 = unlocked
        public bool IsInteractable;
        public Button.ButtonClickedEvent OnClickEvent;

    }

    public GameObject levelButton;
    public List<Level> levelList;
    public Transform Spacer;


    // Use this for initialization
    void Start()
    {
        //Only uncomment this to delete player prefs
        //DeleteAll();
        FillList();
    }

    void FillList()
    {
        foreach(var level in levelList)
        {
            GameObject newButton = Instantiate(levelButton) as GameObject;
            LevelButton button = newButton.GetComponent<LevelButton>();
            button.LevelText.text = level.LevelText;
            //Level names, example: Level1, Level2, Level3

            if (PlayerPrefs.GetInt("Level " + button.LevelText.text) == 1)
            {
                level.Unlocked = 1;
                level.IsInteractable = true;
            }
            button.unlocked = level.Unlocked;
            button.GetComponent<Button>().interactable = level.IsInteractable;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevels("Level " + button.LevelText.text));

            
             //Update stars based on PlayerPref recorded goals (completion, time, completed without dying)
             if (PlayerPrefs.GetInt("Level " + button.LevelText.text + "_completed") == 1) {
                button.Star1.SetActive(true);
             }
             if (PlayerPrefs.GetInt("Level " + button.LevelText.text + "_timegoal") != 0 && PlayerPrefs.GetInt("Level " + button.LevelText.text + "_timegoal") < 10) {
                button.Star2.SetActive(true);
             }
             if (PlayerPrefs.GetInt("Level " + button.LevelText.text + "_deathless") == 1) {
                button.Star3.SetActive(true);
             }
             
             //Maybe include a best time
             

            newButton.transform.SetParent(Spacer, false);
        }
        SaveAll();
    }

    void SaveAll()
    {
        GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject buttons in allButtons)
        {
            LevelButton button = buttons.GetComponent<LevelButton>();
            PlayerPrefs.SetInt("Level " + button.LevelText.text, button.unlocked);
        }
    }

    void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    void LoadLevels(string value)
    {
        SceneManager.LoadScene(value);
    }

}
