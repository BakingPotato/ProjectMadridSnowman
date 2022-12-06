using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : PlayerHealthBar
{

    public new void SetHealth(int health)
    {
        float lastV = slider.value;
        slider.value = slider.maxValue - health;
        if (lastV < slider.value)
		{
            GetComponent<Animation>().Play();
        }
    }
}
