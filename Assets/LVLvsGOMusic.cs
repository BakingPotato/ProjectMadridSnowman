using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLvsGOMusic : MonoBehaviour
{
    [SerializeField] GameObject _lvlMusic;
    [SerializeField] GameObject _altMusic1;
    [SerializeField] GameObject _altMusic2;
    [SerializeField] GameObject _altMusic3;


    void Start()
    {
        _lvlMusic.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_altMusic1.activeSelf | _altMusic2.activeSelf | _altMusic3.activeSelf)
        {
            _lvlMusic.SetActive(false);
        }
    }
}
