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

    private void Start()
    {
	}

    public void SetScene(string name)
	{
		StartCoroutine(SceneTransition(name));
	}

	IEnumerator SceneTransition(string sceneName)
	{
		transitionAnimator.SetTrigger("Start");
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		transitionAnimator.SetTrigger("End");
	}
}