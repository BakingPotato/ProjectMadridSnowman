using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _pointsText;
    [SerializeField] TextMeshProUGUI _healthText;

    public void UpdatePointsText(int points)
	{
		_pointsText.text = "Points: " + points;
	}

	public void UpdateHealthText(int health)
	{
		_healthText.text = "HP: " + health;
	}
}
