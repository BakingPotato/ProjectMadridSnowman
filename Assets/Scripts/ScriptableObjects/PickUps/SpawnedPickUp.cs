using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedPickUp : MonoBehaviour
{
    [SerializeField] PickUpObject pickUpEffect;
 
    public virtual void OnPickupCallback()
    {
        pickUpEffect.Apply(GameObject.FindGameObjectWithTag("Player"));
    }
}
