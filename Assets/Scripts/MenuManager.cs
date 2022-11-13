using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;

	private void Start()
	{
		musicSlider.value = AudioManager.Instance.MusicVolume;
		sfxSlider.value = AudioManager.Instance.SFXVolume;
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
		AudioManager.Instance.MusicVolume = v;
	}
	public void SetSFXVolume(float v)
	{
		AudioManager.Instance.SFXVolume = v;
	}
}
