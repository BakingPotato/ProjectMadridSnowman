using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] PickUpObject pickUpEffect;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
        {
			pickUpEffect.Apply(other.gameObject);
			Destroy(transform.parent.gameObject);
		}
	}
}
