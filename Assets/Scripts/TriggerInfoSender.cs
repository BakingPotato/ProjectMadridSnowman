using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInfoSender : MonoBehaviour
{
	[SerializeField] string message;
	[SerializeField] float messageDuration;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameManager.Instance.CurrentLevelManager.UIManager.SendTextInfo(message, messageDuration);
		}
	}
}
