using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static GameManager GM;

    [SerializeField] TextMeshProUGUI _pointsText;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] TextMeshProUGUI _timerText;

	[Header("Paneles UI")]
	[SerializeField] GameObject gameOverPanel;
	[SerializeField] GameObject gamePanel;
	[SerializeField] GameObject pausePanel;

	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;

	private void Start()
    {
		GM = GameManager.Instance;
		musicSlider.value = AudioManager.Instance.MusicVolume;
		sfxSlider.value = AudioManager.Instance.SFXVolume;
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

	public void ShowGameOver()
	{
		gamePanel.SetActive(false);
		gameOverPanel.SetActive(true);
	}

	public void ShowPause(bool b)
	{
		pausePanel.SetActive(b);
	}

	public void SetMusicVolume(float v)
	{
		AudioManager.Instance.MusicVolume = v;
	}
	public void SetSFXVolume(float v)
	{
		AudioManager.Instance.SFXVolume = v;
	}

	public void SwitchScene(string name)
	{
		Time.timeScale = 1;
		GameManager.Instance.SetScene(name);
	}
}
