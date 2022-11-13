using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	[SerializeField] GameObject creditsPanel;

    public void SwitchScene(string name)
	{
		GameManager.Instance.SetScene(name);
	}

	public void ShowCredits()
	{
		creditsPanel.SetActive(true);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
