using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

    public float timer = 2.5f;

    void Start()
    {
        StartCoroutine(WaitAndDestory(timer));
    }

    private IEnumerator WaitAndDestory(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
