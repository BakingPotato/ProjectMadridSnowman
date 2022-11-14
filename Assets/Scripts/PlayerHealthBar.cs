using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = 0;
    }

    public void SetHealth(int health)
    {
        slider.value = slider.maxValue - health;
    }
}
