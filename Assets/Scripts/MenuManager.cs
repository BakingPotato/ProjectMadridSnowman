using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
	[Header("Botones de opciones")]
	[SerializeField] GameObject audioPanel;
	[SerializeField] GameObject videoPanel;

	[Header("Opciones de audio")]
	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;

	[Header("Variables de intro")]
	[SerializeField] GameObject introPanel;
	[SerializeField] VideoPlayer introVideo;
	[SerializeField] float introTime;

	private void Start()
	{
		//musicSlider.value = AudioManager.Instance.MusicVolume;
		//sfxSlider.value = AudioManager.Instance.SFXVolume;

		if (GameManager.Instance.IntroVideo && introVideo != null)
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

	public void PlayButtonSound()
	{
		AudioManager.Instance.PlaySFX("Button");
	}

	public void showAudioSettings()
    {
		audioPanel.SetActive(true);
		videoPanel.SetActive(false);
    }

	public void showVideoSettings()
	{
		audioPanel.SetActive(false);
		videoPanel.SetActive(true);
	}
}
