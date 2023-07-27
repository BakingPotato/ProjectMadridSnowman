using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class EndPoint : MonoBehaviour
{
	[SerializeField] string nextSceneName;
	[SerializeField] int minPoints;
	private void OnTriggerEnter(Collider other)
	{
		PlayerManager pM = other.GetComponent<PlayerManager>();
		if (pM && GameManager.Instance.CurrentLevelManager.Points >= minPoints)
			GameManager.Instance.ShowResults(nextSceneName);
		else if (pM)
        {
			if(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
            {
				GameManager.Instance.CurrentLevelManager.UIManager.SendTextInfo("Necesito " + minPoints / 100 + "," + (minPoints % 100).ToString("00") + " euros para pasar a la estación.", 4.5f);
			}
			else
            {
				GameManager.Instance.CurrentLevelManager.UIManager.SendTextInfo("I need " + minPoints / 100 + "," + (minPoints % 100).ToString("00") + " euros to enter the station.", 4.5f);
			}
		}

	}
}
