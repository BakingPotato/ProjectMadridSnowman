using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using FMOD.Studio;
using FMODUnity;

public class MusicLogic : MonoBehaviour
{   
    public static MusicLogic instance = null;
    private GameObject intro;
    private GameObject creditos;
    private Scene escena;
    private EventInstance _musicaMenu;
    private EventInstance _musicaCreditos;
    private FMOD.Studio.Bus _musicBus;
    

    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        // Otherwise, its a double, we dont need it - destroy
        else
        {
            Destroy(this.gameObject) ;
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Play(EventInstance evento)
    {
        PLAYBACK_STATE estado;
        evento.getPlaybackState(out estado);
        if (estado != PLAYBACK_STATE.PLAYING)
        {
            evento.start();
        }
    }

    void Start()
    {
        _musicaMenu = RuntimeManager.CreateInstance("event:/MUSIC/MENU/music_menu");
        _musicaCreditos = RuntimeManager.CreateInstance("event:/MUSIC/MENU/music_credits");
        _musicBus = FMODUnity.RuntimeManager.GetBus("bus:/MUSIC");
    }

    void Update()
    {
        escena = SceneManager.GetActiveScene();
        intro = GameObject.FindWithTag("intro");
        creditos = GameObject.FindWithTag("creditos");

        if (SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            if (intro is null)
            {   
                if (creditos is null)
                {
                    Play(_musicaMenu);
                }
                else
                {
                    Play(_musicaCreditos);
                }
            }
        }
        else
        {
            _musicaMenu.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
