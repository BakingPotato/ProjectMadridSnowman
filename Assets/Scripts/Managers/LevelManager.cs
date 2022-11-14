using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    bool gameOver = false;
    bool gamePaused = false;

	[SerializeField] UIManager uIManager;
	public Countdown countdown;

    [SerializeField] GameObject effectsCamera;

    const int MAX_POINTS = 999;
    int _points;

    public const int MAX_HEALTH = 10;
    int _health;

    int _killCount;
    int _totalDamage;

	public int Points { get => _points; set {
            _points = value;
            if (_points < 0)
                _points = 0;
            else if (_points > MAX_POINTS)
                _points = MAX_POINTS;
            UIManager.UpdatePointsText(_points);
        }
    }

    public int HP
    {
        get => _health;
        set
        {
            if(!gameOver)
            {
                _health = value;
                if (_health <= 0)
                {
                    startGameOver(false);
                }
                else if (_health > MAX_HEALTH)
                    _health = MAX_HEALTH;
                UIManager.UpdateHealth(_health);
            }
            
        }
    }

    public int KillCount { get => _killCount; set => _killCount = value; }
    public int TotalDamage { get => _totalDamage; set => _totalDamage = value; }
	public UIManager UIManager { get => uIManager; set => uIManager = value; }

	// Start is called before the first frame update
	void Start()
    {
        HP = MAX_HEALTH;

        //La camara de efectos debe empezar desactivada para no fallar la rotación del personaje
        effectsCamera.SetActive(true);

        //Inicializamos el contador
        countdown = GetComponent<Countdown>();
        countdown.BeginTimer();

        GameManager.Instance.CurrentLevelManager = this;

        AudioManager.Instance.PlayMusic(SceneManager.GetActiveScene().name);
    }

	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
  //          GameManager.Instance.SetScene("Patata");
		//}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (countdown.getElapsedTime() <= 0 && !gameOver)
        {
            countdown.StopTimer();
            startGameOver(true);
        }
    }

    public void startGameOver(bool alive)
    {
        gameOver = true;
        UIManager.ShowGameOver(alive);
        AudioManager.Instance.PlayMusic("GameOver");
    }

    public bool getGameOver()
    {
        return gameOver;
    }

    public void setGameOver(bool enabled)
    {
        gameOver = enabled;
    }

    public void TogglePause()
	{
        if (!GameManager.Instance.CanPause())
            return;
        gamePaused = !gamePaused;
        Time.timeScale = (gamePaused) ? 0 : 1;
        UIManager.ShowPause(gamePaused);
	}

}
