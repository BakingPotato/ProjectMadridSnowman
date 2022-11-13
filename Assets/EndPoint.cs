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
	}
}
