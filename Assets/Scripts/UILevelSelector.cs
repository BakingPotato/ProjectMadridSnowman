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
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject lockedButton;
    [SerializeField] GameObject lockedPanel;

    //DEBUG
    private string[] cheatCode = new string[] { "s", "n", "o", "w"};
    private int index;

    private void Start()
	{
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIndex].levelName);
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
            DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIndex].levelName);
            index = 0;
        }
    }

	//DEBUG
	public void ResetSaveFile()
	{
        GameManager.Instance.NewLevelsData();
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIndex].levelName);
    }

	public void PlayLevel()
	{
        GameManager.Instance.SetScene(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIndex].sceneName);
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
            descriptionText.text = "????";
            recordText.text = "Record: 0";
        }
		else
		{
            lockedButton.SetActive(false);
            lockedPanel.SetActive(false);
            playButton.SetActive(true);
            descriptionText.text = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIndex].description;
            recordText.text = "Record: " + displayLevel.record;
        }

        backgroundImg.sprite = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIndex].sprite;
        levelNameText.text = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIndex].levelName;
    }

    public void NextLevel()
	{
        GameManager.Instance.CurrentLevelIndex++;
        if (GameManager.Instance.CurrentLevelIndex >= GameManager.Instance.LevelsSO.Length)
            GameManager.Instance.CurrentLevelIndex = 0;
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIndex].levelName);
	}

    public void PreviousLevel()
    {
        GameManager.Instance.CurrentLevelIndex--;
        if (GameManager.Instance.CurrentLevelIndex < 0)
            GameManager.Instance.CurrentLevelIndex = GameManager.Instance.LevelsSO.Length - 1;
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIndex].levelName);
    }
}
