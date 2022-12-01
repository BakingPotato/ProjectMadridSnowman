using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image frostIMG;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = 0;
    }

    public void SetHealth(int health)
    {
        float lastV = slider.value;
        slider.value = slider.maxValue - health;
        if (lastV < slider.value)
		{
            GetComponent<Animation>().Play();
        }

        Color alphaC = frostIMG.color;
        alphaC.a = slider.normalizedValue;
        frostIMG.color = alphaC;
    }
}
