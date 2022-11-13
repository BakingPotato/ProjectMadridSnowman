using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
	private static GameManager GM;

    [SerializeField] TextMeshProUGUI _pointsText;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] PlayerHealtBar _healthBar;

	[Header("Resultados")]
	[SerializeField] TextMeshProUGUI _timeLeftText;
	[SerializeField] TextMeshProUGUI _moneyText;
	[SerializeField] TextMeshProUGUI _enemiesText;
	[SerializeField] TextMeshProUGUI _damageText;
	[SerializeField] TextMeshProUGUI _totalText;
	[SerializeField] Animator resultsAnim;

	[Header("Paneles UI")]
	[SerializeField] GameObject gameOverPanel;
	[SerializeField] GameObject gamePanel;
	[SerializeField] GameObject pausePanel;
	[SerializeField] GameObject resultsPanel;

	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;

	private void Start()
    {
		GM = GameManager.Instance;
		musicSlider.value = AudioManager.Instance.MusicVolume;
		sfxSlider.value = AudioManager.Instance.SFXVolume;
		_healthBar.SetMaxHealth(LevelManager.MAX_HEALTH);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.ShowResults("MainMenu");
        }
    }

	public void UpdatePointsText(int points)
	{
		_pointsText.text = points.ToString();
	}

	public void UpdateHealth(int health)
	{
		_healthBar.SetHealth(health);
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

	public void ShowResults(string timeLeft, string money, string enemies, string damage, string total, string nextSceneName)
    {
		resultsPanel.SetActive(true);
		_timeLeftText.text = timeLeft;
		_moneyText.text = money;
		_enemiesText.text = enemies;
		_damageText.text = "- " + damage;
		_totalText.text = total;

		resultsAnim.SetTrigger("showResults");
		//_continueButton.onClick.AddListener(delegate () { SwitchScene(nextSceneName); });
	}

	public void SwitchScene(string name)
	{
		Time.timeScale = 1;
		GameManager.Instance.SetScene(name);
	}
}
