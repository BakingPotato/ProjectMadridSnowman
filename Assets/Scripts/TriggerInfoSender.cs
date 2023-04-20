using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class TriggerInfoSender : MonoBehaviour
{
	[SerializeField] [TextArea] string messageES;
	[SerializeField] [TextArea] string messageEN;
	[SerializeField] float messageDuration;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			//Español
			if(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
            {
				GameManager.Instance.CurrentLevelManager.UIManager.SendTextInfo(messageES, messageDuration);

			}
			else
            {
				GameManager.Instance.CurrentLevelManager.UIManager.SendTextInfo(messageEN, messageDuration);
			}
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Objects/info_box");
		}
	}
}
