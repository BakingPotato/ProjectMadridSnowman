using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float y;
    [SerializeField] float rotationSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        y += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }
}
