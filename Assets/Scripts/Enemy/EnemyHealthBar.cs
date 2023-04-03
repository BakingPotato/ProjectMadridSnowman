using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] float showTime;
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    [SerializeField] bool boss = false;

	public void SetMaxHealth(int health)
	{
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(int health)
	{
        parent.SetActive(true);

        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);

        CancelInvoke();
        if (health > 0)
        {
            Invoke("HideAfterSeconds", showTime);
        }
        else
        {
            Invoke("HideAfterSeconds", 0.15f);
        }
    }

    void HideAfterSeconds()
	{
        if (!boss)
        {
            parent.SetActive(false);
        }
    }
}
