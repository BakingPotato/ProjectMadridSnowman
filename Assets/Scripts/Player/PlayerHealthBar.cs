using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] protected Slider slider;
    [SerializeField] private Image frostIMG;

    public virtual void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = 0;
    }

    public virtual void SetHealth(int health)
    {
        float lastV = slider.value;
        slider.value = slider.maxValue - health;
        if (lastV < slider.value)
		{
            GetComponent<Animation>().Play();
        }

        Color alphaC = frostIMG.color;
        alphaC.a = slider.normalizedValue/1.8f;
        frostIMG.color = alphaC;
    }
}
