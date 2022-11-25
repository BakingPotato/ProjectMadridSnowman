using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMenuMusic : MonoBehaviour
{
    [SerializeField] GameObject _audio;
    [SerializeField] GameObject _intro;
    [SerializeField] GameObject _credits;

    void Start()
    {
        _audio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_intro.activeSelf)
        {
            _audio.SetActive(false);
        }
        else
        {
            _audio.SetActive(true);
            if (_credits.activeSelf)
            {
                _audio.SetActive(false);
                //Debug.Log(_audio.activeSelf);
            }
            else
            {
                _audio.SetActive(true);
                //Debug.Log(_audio.activeSelf);
            }
        }
    }
}
