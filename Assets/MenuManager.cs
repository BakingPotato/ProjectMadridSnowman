using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void SwitchScene(string name)
	{
		GameManager.Instance.SetScene(name);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
