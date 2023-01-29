using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
	[SerializeField] string nextSceneName;
	[SerializeField] int minPoints;
	public ParticleSystem EndPS;
	private void OnTriggerEnter(Collider other)
	{
		PlayerManager pM = other.GetComponent<PlayerManager>();
		if (pM && GameManager.Instance.CurrentLevelManager.Points >= minPoints)
			GameManager.Instance.ShowResults(nextSceneName);
		else if(pM)
			GameManager.Instance.CurrentLevelManager.UIManager.SendTextInfo("Necesito " + minPoints / 100 + "," + (minPoints % 100).ToString("00") + " euros para pasar a la estación.", 4.5f);
		ParticleSystem ParSis = Instantiate(EndPS);
		ParSis.transform.position = transform.position;
		ParSis.Play();
	}
}
