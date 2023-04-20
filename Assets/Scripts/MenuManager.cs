using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using TMPro;

public class MenuManager : MonoBehaviour
{
	[Header("Botones de opciones")]
	[SerializeField] GameObject audioPanel;
	[SerializeField] GameObject videoPanel;
	[SerializeField] GameObject otherPanel;

	[Header("Opciones de audio")]
	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;

	[Header("Variables de intro")]
	[SerializeField] GameObject introPanel;
	[SerializeField] VideoPlayer introVideo;
	[SerializeField] float introTime;

	public TMP_Dropdown dropdown_sm;
	public TMP_Dropdown dropdown_language;

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

		if (GameManager.Instance.IntroVideo && introVideo != null || SceneManager.GetActiveScene().name == "DemoEnding")
		{
			GameManager.Instance.IntroVideo = false;
			introPanel.SetActive(true);
			introVideo.Play();
			CancelInvoke();
			Invoke("HideIntroPanel", introTime);
		}

        if (dropdown_sm)
			SetDropdowns();


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

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
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

	public void changeShootingMode(int index)
	{
		//0 = Pulsar
		//1 = Alternar
		PlayerPrefs.SetInt("ShootingMode", index);
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
		otherPanel.SetActive(false);
    }

	public void showVideoSettings()
	{
		audioPanel.SetActive(false);
		videoPanel.SetActive(true);
		otherPanel.SetActive(false);
	}

	public void showOtherSettings()
	{
		audioPanel.SetActive(false);
		videoPanel.SetActive(false);
		otherPanel.SetActive(true);
	}

	public void changeLanguage(int index)
	{
		//0 = Inglés
		//1 = Español
		LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

		SetDropdowns();
	}

	private void SetDropdowns()
	{

		//Español
		if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
		{

			dropdown_sm.options[0].text = "Pulsar"; dropdown_sm.options[1].text = "Alternar";
			dropdown_language.options[0].text = "Inglés"; dropdown_language.options[1].text = "Español";

			dropdown_language.value = 1;
		}
		else
		{

			dropdown_sm.options[0].text = "Hold"; dropdown_sm.options[1].text = "Switch";
			dropdown_language.options[0].text = "English"; dropdown_language.options[1].text = "Spanish";

			dropdown_language.value = 0;
		}

		if (PlayerPrefs.GetInt("ShootingMode", 0) == 0)
		{
			dropdown_sm.value = 0;
		}
		else
		{
			dropdown_sm.value = 1;
		}
	}
}