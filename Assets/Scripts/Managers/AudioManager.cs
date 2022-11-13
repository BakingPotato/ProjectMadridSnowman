using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
	const float DEFAULT_VOLUME = 0.5f;

	private static AudioManager _instance;
	public static AudioManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("Null AudioManager");
			return _instance;
		}
	}

	[SerializeField] Sound[] musicSounds, sfxSounds;
	[SerializeField] AudioSource musicSource, sfxSource;

	public float MusicVolume
	{
		get => musicSource.volume; set
		{
			musicSource.volume = value;
			PlayerPrefs.SetFloat("MusicVolume", musicSource.volume);
		}
	}
	public float SFXVolume
	{
		get => sfxSource.volume; set
		{
			sfxSource.volume = value;
			PlayerPrefs.SetFloat("SFXVolume", sfxSource.volume);
		}
	}

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

		MusicVolume = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : DEFAULT_VOLUME;
		SFXVolume = PlayerPrefs.HasKey("SFXVolume") ? PlayerPrefs.GetFloat("SFXVolume") : DEFAULT_VOLUME;
	}

	private void Start()
	{
		PlayMusic("Level1");	
	}

	public void PlayMusic(string name)
	{
		Sound s = Array.Find(musicSounds, x => x.name == name);

		if (s != null)
		{
			musicSource.clip = s.clip;
			musicSource.Play();
		}
		else
			Debug.LogError("Music named " + name + " not found.");
	}

	public void PlaySFX(string name)
	{
		Sound s = Array.Find(sfxSounds, x => x.name == name);

		if (s != null)
			sfxSource.PlayOneShot(s.clip);
		else
			Debug.LogError("SFX named " + name + " not found.");
	}

}
