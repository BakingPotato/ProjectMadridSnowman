using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UILevelSelector : MonoBehaviour
{
    [SerializeField] Image backgroundImg;
    [SerializeField] TextMeshProUGUI levelNameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI recordText;
    [SerializeField] TextMeshProUGUI averageRecordText;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject lockedButton;
    [SerializeField] GameObject lockedPanel;

    //DEBUG
    private string[] cheatCode = new string[] { "s", "n", "o", "w"};
    private int index;

    private void Start()
	{
        if(PlayerPrefs.GetInt("GameFinished", 0) == 1)
        {
            DisplayAverageRecord();
        }
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName);
	}

	private void Update()
	{
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Check if the next key in the code is pressed
            if (Input.GetKeyDown(cheatCode[index]))
            {
                // Add 1 to index to check the next key in the code
                index++;
            }
            // Wrong key entered, we reset code typing
            else
            {
                index = 0;
            }
        }
        // If index reaches the length of the cheatCode string, 
        // the entire code was correctly entered
        if (index == cheatCode.Length)
        {
            // Cheat code successfully inputted!
            GameManager.Instance.UnlockAllLevels();
            DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName);
            DisplayAverageRecord();
            index = 0;
        }
    }

	//DEBUG
	public void ResetSaveFile()
	{
        GameManager.Instance.NewLevelsData();
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName);
    }

	public void PlayLevel()
	{
        GameManager.Instance.SetScene(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].sceneName);
	}

    void DisplayLevel(string levelName)
	{
        SaveManager.LevelData displayLevel;
        displayLevel.levelName = "";
        displayLevel.record = 0;
        displayLevel.unlocked = false;

        foreach (SaveManager.LevelData levelData in SaveManager.GameDataInstance.levels)
		{
            if (levelName == levelData.levelName)
			{
                displayLevel = levelData;
                break;
            }
		}

        if (displayLevel.levelName == "")
            Debug.LogError("Level not found in saved file.");

		if (!displayLevel.unlocked)
		{
            lockedButton.SetActive(true);
            lockedPanel.SetActive(true);
            playButton.SetActive(false);
            descriptionText.text = "???";
            levelNameText.text = "???";
            recordText.text = "Record: 0";
        }
		else
		{
            lockedButton.SetActive(false);
            lockedPanel.SetActive(false);
            playButton.SetActive(true);
            descriptionText.text = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].description;
            recordText.text = "Record: " + displayLevel.record;
            levelNameText.text = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName;
        }

        backgroundImg.sprite = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].sprite;
    }

    void DisplayAverageRecord()
    {
        int allRecordSum = 0;
        int levelNumber = 0;

        foreach (SaveManager.LevelData levelData in SaveManager.GameDataInstance.levels)
        {
            levelNumber++;
            allRecordSum += levelData.record;
        }
        averageRecordText.text = "Puntuación Media:            " + (allRecordSum / levelNumber).ToString();
        averageRecordText.gameObject.SetActive(true);
    }

    public void NextLevel()
	{
        GameManager.Instance.CurrentLevelIdx++;
        
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName);
	}

    public void PreviousLevel()
    {
        GameManager.Instance.CurrentLevelIdx--;
        
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName);
    }
}
