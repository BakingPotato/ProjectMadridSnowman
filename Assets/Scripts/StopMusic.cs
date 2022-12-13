using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMOD.Studio;
using FMODUnity;

public class StopMusic : MonoBehaviour
{
    [SerializeField] GameObject _musicaPorDefecto;
    [SerializeField] GameObject _objetoQueImpide;
    [SerializeField] GameObject _musicaAlternativa;

    void Start()
    {
        _musicaPorDefecto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_objetoQueImpide.activeSelf)
        {
            _musicaPorDefecto.SetActive(false);
        }
        else
        {
            if (_musicaAlternativa.activeSelf)
            {
                _musicaPorDefecto.SetActive(false);
                //Debug.Log(_musicaPorDefecto.activeSelf);
            }
            else
            {
                _musicaPorDefecto.SetActive(true);
                //Debug.Log(_musicaPorDefecto.activeSelf);
            }
        }
    }
}
