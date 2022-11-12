using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	private static GameManager GM;

    [SerializeField] TextMeshProUGUI _pointsText;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] TextMeshProUGUI _timerText;

    private void Start()
    {
		GM = GameManager.Instance;
	}

	public void UpdatePointsText(int points)
	{
		_pointsText.text = "Points: " + points;
	}

	public void UpdateHealthText(int health)
	{
		_healthText.text = "HP: " + health;
	}

	public void UpdateTimerText(string time)
	{
		_timerText.text = time;
	}

	public void replayButton()
    {
		GM.SetScene(SceneManager.GetActiveScene().name);
    }
}
