using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System;
using UnityEditor;
using DG.Tweening;

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
    [SerializeField] CanvasGroup panelCG;

    //DEBUG
    private string[] cheatCode = new string[] { "s", "n", "o", "w"};
    private int index;

    private void Start()
	{
        if (PlayerPrefs.GetInt("GameFinished", 0) == 1)
        {
            DisplayAverageRecord();
        }
        GameManager.Instance.onFileUpdate += UpdateDisplayLevel;
        UpdateDisplayLevel();
    }

    private void OnDisable()
    {
        GameManager.Instance.onFileUpdate -= UpdateDisplayLevel;
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
            index = 0;
        }
    }

	public void PlayLevel()
	{
        GameManager.Instance.SetScene(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].sceneName);
	}

    void UpdateDisplayLevel()
    {
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName);
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
            //Español
            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
            {
                descriptionText.text = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].description;
                levelNameText.text = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName;
            }
            else
            {
                descriptionText.text = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].description_EN;
                levelNameText.text = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName_EN;
            }
            recordText.text = "Record: " + displayLevel.record;

        }

        backgroundImg.sprite = GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].sprite;
    }

    public void NextLevel()
	{
        GameManager.Instance.CurrentLevelIdx++;
        
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName);

        panelCG.alpha = 0;
        panelCG.DOFade(1, 0.3f);
    }

    public void PreviousLevel()
    {
        GameManager.Instance.CurrentLevelIdx--;
        
        DisplayLevel(GameManager.Instance.LevelsSO[GameManager.Instance.CurrentLevelIdx].levelName);

        panelCG.alpha = 0;
        panelCG.DOFade(1, 0.3f);
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
        averageRecordText.text = (allRecordSum / levelNumber).ToString();
        averageRecordText.transform.parent.gameObject.SetActive(true);
    }
}
