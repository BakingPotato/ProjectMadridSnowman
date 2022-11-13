using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInfoSender : MonoBehaviour
{
	[SerializeField] UIManager uIManager;
	[SerializeField] string message;
	[SerializeField] float messageDuration;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			uIManager.SendTextInfo(message, messageDuration);
		}
	}
}
