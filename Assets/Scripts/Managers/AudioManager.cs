/*
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

	public void PlayMusic(string name)
	{
		Debug.Log(name + "playing");
		Sound s = Array.Find(musicSounds, x => x.name == name);

		if (s != null)
		{
			musicSource.clip = s.clip;
			musicSource.Play();
		}
		else
			Debug.LogWarning("Music named " + name + " not found.");
	}

	public void PlaySFX(string name)
	{
		Sound s = Array.Find(sfxSounds, x => x.name == name);

		if (s != null)
			sfxSource.PlayOneShot(s.clip);
		else
			Debug.LogWarning("SFX named " + name + " not found.");
	}

	public void PlaySFX3D(string name, Vector3 position)
	{
		Sound s = Array.Find(sfxSounds, x => x.name == name);

		if (s != null)
		{
			PlayClipAt(s.clip, position, SFXVolume);
		}
		else
			Debug.LogWarning("SFX named " + name + " not found.");
	}

	public void PlaySFXRandomPitch(string name)
	{
		Sound s = Array.Find(sfxSounds, x => x.name == name);

		if (s != null)
		{
			float pitch = sfxSource.pitch;
			sfxSource.pitch = UnityEngine.Random.Range(0.6f, 1.4f);
			sfxSource.PlayOneShot(s.clip);
			sfxSource.pitch = pitch;
		}
		else
			Debug.LogWarning("SFX named " + name + " not found.");
	}

	public void PlaySFX3DRandomPitch(string name, Vector3 position)
	{
		Sound s = Array.Find(sfxSounds, x => x.name == name);

		if (s != null)
		{
			PlayClipAt(s.clip, position, SFXVolume).pitch = UnityEngine.Random.Range(0.6f, 1.5f);
		}
		else
			Debug.LogWarning("SFX named " + name + " not found.");
	}

	AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float volume)
	{
		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.volume = volume;
		aSource.maxDistance = 25;
		aSource.rolloffMode = AudioRolloffMode.Linear;
		aSource.spatialBlend = 1;
							 // set other aSource properties here, if desired
		aSource.Play(); // start the sound
		Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}
}
 */