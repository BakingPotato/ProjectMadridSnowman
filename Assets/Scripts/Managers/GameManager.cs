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

	LevelManager _currentLevelManager;

	[SerializeField] Animator transitionAnimator;

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

	}

	bool _introVideo = true;

    private void Start()
    {
		Application.targetFrameRate = 60;
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

		int total = (timeLeft * 3) + score + (enemies * 8) + (boxes * 6) - (damage * 10);

		CurrentLevelManager.UIManager.ShowResults(timeLeft.ToString(), score.ToString(), enemies.ToString(), boxes.ToString(), damage.ToString(), total.ToString(), nextSceneName);
	}
}