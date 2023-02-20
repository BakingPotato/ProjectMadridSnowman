using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingProyectile : MonoBehaviour
{
    public float velocity = 5.0f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 1 * velocity * Time.deltaTime, 0));
    }
}
