using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("Null GM");
			return _instance;
		}
	}

	private static GameManager _instance;

	public LevelManager CurrentLevelManager { get => _currentLevelManager; set => _currentLevelManager = value; }
	public bool IntroVideo { get => _introVideo; set => _introVideo = value; }
	public LevelScriptableObject[] LevelsSO { get => levelsSO; set => levelsSO = value; }
	public int CurrentLevelIndex { get => _currentLevelIndex; set => _currentLevelIndex = value; }

	LevelManager _currentLevelManager;

	[SerializeField] Animator transitionAnimator;

	[SerializeField] LevelScriptableObject[] levelsSO;

	bool _introVideo = true;

	int _currentLevelIndex = 0;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}

		if (SaveManager.CheckData())
			SaveManager.ReadData();
		else
			NewLevelsData();
	}

    private void Start()
    {
		Application.targetFrameRate = 60;
	}

	public void NewLevelsData()
	{
		SaveManager.GameDataInstance.levels = new SaveManager.LevelData[levelsSO.Length];
		for (int i = 0; i < levelsSO.Length; i++)
		{
			SaveManager.GameDataInstance.levels[i].levelName = levelsSO[i].levelName;
			SaveManager.GameDataInstance.levels[i].record = 0;
			SaveManager.GameDataInstance.levels[i].unlocked = false;
		}
		//Unlock tutorial
		SaveManager.GameDataInstance.levels[0].unlocked = true;

		SaveManager.WriteData();
	}

	public void UnlockAllLevels()
	{
		for (int i = 0; i < SaveManager.GameDataInstance.levels.Length; i++)
		{
			SaveManager.GameDataInstance.levels[i].unlocked = true;
		}

		SaveManager.WriteData();
	}

	public void CompleteLevel(int score)
	{
		if(score > 0 && SaveManager.GameDataInstance.levels[CurrentLevelIndex].record < score)
			SaveManager.GameDataInstance.levels[CurrentLevelIndex].record = score;
		foreach (string levelName in levelsSO[CurrentLevelIndex].unlocks)
		{
			for (int i = 0; i < SaveManager.GameDataInstance.levels.Length; i++)
			{
				if (levelName == SaveManager.GameDataInstance.levels[i].levelName)
				{
					SaveManager.GameDataInstance.levels[i].unlocked = true;
					break;
				}
					
			}
		}

		SaveManager.WriteData();
	}

	public void SetScene(string name)
	{
		StartCoroutine(SceneTransition(name));
	}

	IEnumerator SceneTransition(string sceneName)
	{
		Time.timeScale = 1;
		transitionAnimator.SetTrigger("Start");
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		transitionAnimator.SetTrigger("End");
	}

	public bool CanPause()
	{
		return transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
	}
	public void ShowResults(string nextSceneName)
	{
		CurrentLevelManager.countdown.StopTimer();
		CurrentLevelManager.setGameOver(true);
		int timeLeft = (int)Mathf.Ceil(CurrentLevelManager.countdown.getElapsedTime());
		int score = CurrentLevelManager.Points;
		int enemies = CurrentLevelManager.KillCount;
		int boxes = CurrentLevelManager.BoxCount;
		int damage = CurrentLevelManager.TotalDamage;

		int total = (timeLeft * 4) + (score * 2) + (enemies * 9) + (boxes * 6) - (damage * 10);
		CompleteLevel(total);
		CurrentLevelManager.UIManager.ShowResults(timeLeft.ToString(), score.ToString(), enemies.ToString(), boxes.ToString(), damage.ToString(), total.ToString(), nextSceneName);
	}
}