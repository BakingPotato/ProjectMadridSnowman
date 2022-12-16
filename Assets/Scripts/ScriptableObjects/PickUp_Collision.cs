using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Collision : MonoBehaviour
{
	[SerializeField] PickUpObject pickUpEffect;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			pickUpEffect.Apply(collision.gameObject);
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "Enemy")
		{
			Destroy(gameObject);
		}
	}
}
