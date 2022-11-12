using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _pointsText;

    public void UpdatePointsText(int points)
	{
		_pointsText.text = "Points: " + points;
	}
}
