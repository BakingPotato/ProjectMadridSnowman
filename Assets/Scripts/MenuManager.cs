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

	FMOD.Studio.Bus sfxBus;
	FMOD.Studio.Bus musicBus;
	//FMOD.Studio.Bus otherBus;

	private void Start()
	{
		sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
		musicBus = FMODUnity.RuntimeManager.GetBus("bus:/MUSIC");
		//otherBus = FMODUnity.RuntimeManager.GetBus("bus:/OTHER");

		musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
		sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

		sfxBus.setVolume(DecibelToLinear(sfxSlider.value));
		musicBus.setVolume(DecibelToLinear(musicSlider.value));
		//otherBus.setVolume(DecibelToLinear(musicSlider.value));

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

	private float DecibelToLinear(float dB)
	{
		float linear = Mathf.Pow(10.0f, dB/20f);
		return linear;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			introVideo.Stop();
			introPanel.SetActive(false);
		}
		//otherBus.setVolume(DecibelToLinear(musicSlider.value));
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
		musicBus.setVolume(DecibelToLinear(musicSlider.value));
		PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
	}
	public void SetSFXVolume(float v)
	{
		//AudioManager.Instance.SFXVolume = v;
		sfxBus.setVolume(DecibelToLinear(sfxSlider.value));
		PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
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
		//AudioManager.Instance.PlaySFX("Button");
		FMODUnity.RuntimeManager.PlayOneShot("event:/OTHER/UI/ui_button");
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
