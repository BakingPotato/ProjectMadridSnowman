using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootingBar : MonoBehaviour
{
    [SerializeField] Slider slider;
	float _displayTime;

	private void Update()
	{
		_displayTime -= Time.deltaTime;

		if (_displayTime <= 0)
			transform.parent.gameObject.SetActive(false);

		slider.value = _displayTime;
	}

	public void DisplayShootingBar(float time)
	{
		_displayTime = time;
		transform.parent.gameObject.SetActive(true);
		slider.maxValue = _displayTime;
		slider.value = _displayTime;
	}
}
