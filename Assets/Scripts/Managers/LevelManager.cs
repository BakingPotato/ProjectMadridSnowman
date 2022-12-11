using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    bool gameOver = true;
    bool _gamePaused = false;

	[SerializeField] string levelName;
	[SerializeField] UIManager uIManager;
	public Countdown countdown;

    [SerializeField] GameObject effectsCamera;

    const int MAX_POINTS = 9999;
    int _points;
    
    //Esto define la vida máxima del jugador
    public int MAX_HEALTH = 12;
    int _health;

    int _killCount;
    int _boxCount;
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
                    //AudioManager.Instance.PlaySFXRandomPitch("PlayerDeath");
                    startGameOver(false);
                }
                else if (_health > MAX_HEALTH)
                    _health = MAX_HEALTH;
                UIManager.UpdateHealth(_health);
            }
            
        }
    }

    public int KillCount { get => _killCount; set => _killCount = value; }
    public int BoxCount { get => _boxCount; set => _boxCount = value; }
    public int TotalDamage { get => _totalDamage; set => _totalDamage = value; }
	public UIManager UIManager { get => uIManager; set => uIManager = value; }
	public bool GamePaused { get => _gamePaused; set {
            _gamePaused = value;
            Time.timeScale = (GamePaused) ? 0 : 1;
            UIManager.ShowPause(GamePaused);
        }
    }

	public string LevelName { get => levelName; set => levelName = value; }

	private void Awake()
	{
        GameManager.Instance.CurrentLevelManager = this;
    }

	// Start is called before the first frame update
	void Start()
    {
        HP = MAX_HEALTH;

        //La camara de efectos debe empezar desactivada para no fallar la rotaci�n del personaje
        effectsCamera.SetActive(true);

        Time.timeScale = 0;
    }

	private void Update()
	{
		if (!gameOver)
		{
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }

            if (countdown.getElapsedTime() <= 0)
            {
                countdown.StopTimer();
                startGameOver(true);
            }
        }
    }

    public void StartLevel()
	{
        //Inicializamos el contador
        countdown = GetComponent<Countdown>();
        countdown.BeginTimer();
        gameOver = false;
        Time.timeScale = 1;
    }

    public void startGameOver(bool alive)
    {
        setGameOver(true);
        UIManager.ShowGameOver(alive);
        //AudioManager.Instance.PlayMusic("GameOver");
    }

    public bool getGameOver()
    {
        return gameOver;
    }

    public void setGameOver(bool enabled)
    {
        gameOver = enabled;
        Time.timeScale = (gameOver) ? 0 : 1;
    }

    public void TogglePause()
	{
        if (!GameManager.Instance.CanPause() && !gameOver)
            return;
        GamePaused = !GamePaused;
	}
}
