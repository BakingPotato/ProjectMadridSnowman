using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

    public float timer = 2.5f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer)
        {
            Destroy(this.gameObject);
        }
    }
}
