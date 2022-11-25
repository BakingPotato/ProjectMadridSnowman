using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSnow : MonoBehaviour
{
    public float timeAlive;

    private void Awake()
    {
        //this.gameObject.SetActive(true);
        StartCoroutine(SnowAlive());
    }

    void Start()
    {


    }

    private void DestroyObject()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/3D/Objects/snowball_hits");
        Destroy(this.gameObject);
    }

    private IEnumerator SnowAlive()
    {
        yield return new WaitForSeconds(timeAlive);
        DestroyObject();
    }
}
