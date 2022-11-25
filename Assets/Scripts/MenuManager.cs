using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;
	[SerializeField] GameObject introPanel;
	[SerializeField] VideoPlayer introVideo;
	[SerializeField] float introTime;

	private void Start()
	{
		//musicSlider.value = AudioManager.Instance.MusicVolume;
		//sfxSlider.value = AudioManager.Instance.SFXVolume;

		if (GameManager.Instance.IntroVideo)
		{
			GameManager.Instance.IntroVideo = false;
			introPanel.SetActive(true);
			introVideo.Play();
			CancelInvoke();
			Invoke("HideIntroPanel", introTime);
		}

		//AudioManager.Instance.PlayMusic("MainMenu");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			introVideo.Stop();
			introPanel.SetActive(false);
		}
	}
	public void SwitchScene(string name)
	{
		GameManager.Instance.SetScene(name);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
	public void SetMusicVolume(float v)
	{
		//AudioManager.Instance.MusicVolume = v;
	}
	public void SetSFXVolume(float v)
	{
		//AudioManager.Instance.SFXVolume = v;
	}

	public void PlayMusic(string name)
	{
		//AudioManager.Instance.PlayMusic(name);
	}

	void HideIntroPanel()
	{
		introVideo.Stop();
		introPanel.SetActive(false);
	}
}
