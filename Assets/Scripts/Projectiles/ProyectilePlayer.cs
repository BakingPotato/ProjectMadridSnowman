using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilePlayer : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch(LayerMask.LayerToName(collision.transform.gameObject.layer))
        {
            case "Enemy":
                Debug.Log("enemyCollision");
                break;
            case "":
                break;
            default:
                break;
        }
    }
}
