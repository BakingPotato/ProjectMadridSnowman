using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
	private static GameManager GM;

    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] TextMeshProUGUI _pointsText;
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
	}

	public void StartLevelFromUI()
	{
		GM.CurrentLevelManager.StartLevel();
		_titleText.text = GM.CurrentLevelManager.LevelName;
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
		_pointsText.text = points/100 + "," + (points%100).ToString("00");
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

	public void replayButton()
    {
		GM.SetScene(SceneManager.GetActiveScene().name);
    }

	public void ShowGameOver(bool alive)
	{
		gamePanel.SetActive(false);
		if (alive)
			gameOverPanel.SetActive(true);
		else
			deathGameOverPanel.SetActive(true);
	}

	public void ShowPause(bool b)
	{
		pausePanel.SetActive(b);
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
		_timeLeftText.text = timeLeft;
		_moneyText.text = money;
		_enemiesText.text = enemies;
		_boxesText.text = boxes;
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
