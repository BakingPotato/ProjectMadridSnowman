using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
	[SerializeField] string nextSceneName;
	[SerializeField] int minPoints;
	private void OnTriggerEnter(Collider other)
	{
		PlayerManager pM = other.GetComponent<PlayerManager>();
		if (pM && GameManager.Instance.CurrentLevelManager.Points >= minPoints)
			GameManager.Instance.ShowResults(nextSceneName);
		else if(pM)
			GameManager.Instance.CurrentLevelManager.UIManager.SendTextInfo("Necesitas como m�nimo " + minPoints / 100 + "," + (minPoints % 100).ToString("00") + " para pasar a la estaci�n.", 4.5f);
	}
}
