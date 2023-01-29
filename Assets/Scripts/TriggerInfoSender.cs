using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInfoSender : MonoBehaviour
{
	[SerializeField] [TextArea] string message;
	[SerializeField] float messageDuration;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameManager.Instance.CurrentLevelManager.UIManager.SendTextInfo(message, messageDuration);
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Objects/info_box");
		}
	}
}
