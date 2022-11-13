using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaController : MonoBehaviour
{
    public GameObject player;

    public float speed = 3f;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position;
        transform.Rotate(Vector3.up * speed);
    }
}
