using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
	private static GameManager GM;

	[Header("Game UI")]
	[SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] TextMeshProUGUI _pointsText;
	[SerializeField] bool money = true;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] PlayerHealthBar _healthBar;
    [SerializeField] PlayerShootingBar _shootingBar;

	[Header("Resultados")]
	[SerializeField] TextMeshProUGUI _timeLeftText;
	[SerializeField] TextMeshProUGUI _moneyText;
	[SerializeField] TextMeshProUGUI _enemiesText;
	[SerializeField] TextMeshProUGUI _boxesText;
	[SerializeField] TextMeshProUGUI _damageText;
	[SerializeField] TextMeshProUGUI _totalText;
	[SerializeField] Animator resultsAnim;

	[Header("Paneles UI")]
	[SerializeField] GameObject gameOverPanel;
	[SerializeField] GameObject deathGameOverPanel;
	[SerializeField] GameObject gamePanel;
	[SerializeField] GameObject pausePanel;
	[SerializeField] GameObject settingsPanel;
	[SerializeField] GameObject resultsPanel;
	[SerializeField] UITextInfo uITextInfo;

	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;

	FMOD.Studio.Bus sfxBus;
	FMOD.Studio.Bus musicBus;
	//FMOD.Studio.Bus otherBus;

	private void Start()
    {

		sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
		musicBus = FMODUnity.RuntimeManager.GetBus("bus:/MUSIC");
		//otherBus = FMODUnity.RuntimeManager.GetBus("bus:/OTHER");

		GM = GameManager.Instance;

		musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
		sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

		sfxBus.setVolume(DecibelToLinear(sfxSlider.value));
		musicBus.setVolume(DecibelToLinear(musicSlider.value));

		_healthBar.SetMaxHealth(GM.CurrentLevelManager.MAX_HEALTH);

		_titleText.text = GM.LevelsSO[GM.CurrentLevelIdx].levelName;
	}

	public void StartLevelFromUI()
	{
		GM.CurrentLevelManager.StartLevel();
	}

	private float DecibelToLinear(float dB)
	{
		float linear = Mathf.Pow(10.0f, dB/20f);
		return linear;
	}

	private void Update()
	{
		sfxBus.setVolume(DecibelToLinear(sfxSlider.value));
		musicBus.setVolume(DecibelToLinear(musicSlider.value));
		//otherBus.setVolume(DecibelToLinear(musicSlider.value));
	}

	public void UpdatePointsText(int points)
	{
        if (money)
        {
			_pointsText.text = points / 100 + "," + (points % 100).ToString("00");

		}
		else
        {
			_pointsText.text = points.ToString();
		}
	}

	public void UpdateHealth(int health)
	{
		_healthBar.SetHealth(health);
	}

	public void UpdateTimerText(string time)
	{
		if(_timerText != null)
        {
			_timerText.text = time;
		}
	}
	public void SetTimerColor(Color c)
	{
		if (_timerText != null)
		{
			_timerText.color = c;
		}
	}

	public void replayButton()
    {
		GM.SetScene(SceneManager.GetActiveScene().name);
    }

	public void ShowGameOver(bool alive)
	{
		gamePanel.SetActive(false);
		if (alive)
			if (money)
				gameOverPanel.SetActive(true);
			else
				GameManager.Instance.ShowResults("LevelMenu");
		else
			deathGameOverPanel.SetActive(true);
	}

	public void ShowPause(bool b)
	{
		pausePanel.SetActive(b);
		settingsPanel.SetActive(false);
	}

	public void SetMusicVolume(float v)
	{
		//AudioManager.Instance.MusicVolume = v;
		musicBus.setVolume(DecibelToLinear(musicSlider.value));
		PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
	}
	public void SetSFXVolume(float v)
	{
		//AudioManager.Instance.SFXVolume = v;
		sfxBus.setVolume(DecibelToLinear(sfxSlider.value));
		PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
	}

	public void ShowResults(string timeLeft, string money, string enemies, string boxes, string damage, string total, string nextSceneName)
    {
		resultsPanel.SetActive(true);
		if (_timeLeftText)
			_timeLeftText.text = timeLeft;
		if (_moneyText)
			_moneyText.text = money;
		if (_enemiesText)
			_enemiesText.text = enemies;
		if(_boxesText)
			_boxesText.text = boxes;
		if (_damageText)
			_damageText.text = "- " + damage;
		_totalText.text = total;

		resultsAnim.SetTrigger("showResults");
		//_continueButton.onClick.AddListener(delegate () { SwitchScene(nextSceneName); });
	}

	public void SwitchScene(string name)
	{
		GameManager.Instance.SetScene(name);
	}

	public void UITogglePause()
	{
		GameManager.Instance.CurrentLevelManager.TogglePause();
	}

	public void SendTextInfo(string message, float duration)
	{
		uITextInfo.SendInfo(message, duration);
	}

	public void ShowShootingBar(float time)
	{
		_shootingBar.DisplayShootingBar(time);
	}

	public void PlayButtonSound()
	{
		//AudioManager.Instance.PlaySFX("Button");
		FMODUnity.RuntimeManager.PlayOneShot("event:/OTHER/UI/ui_button");

	}
}
