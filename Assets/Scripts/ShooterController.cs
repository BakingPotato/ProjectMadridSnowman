using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [Range(0.5f, 10.0f)]
    public float shotSpeed;

    [Range(1.0f, 10.0f)]
    public float reloadTime;

    [Tooltip("Instance of the player to get his/her rotation")]
    public GameObject playerInstance;

    [Tooltip("Snow model prefab")]
    public GameObject snowballPrefab;



    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public bool Launch()
    {
        return false;
    }
}
