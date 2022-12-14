using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Collision : MonoBehaviour
{
	public PickUpObject pickUpEffect;

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

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "PowerUp")
		{
			pickUpEffect.Apply(GameObject.FindGameObjectWithTag("Player"));
			Destroy(gameObject);
		}
	}
}
