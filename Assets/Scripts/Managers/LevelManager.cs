using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    bool gameOver = false;

    public UIManager UIManager;
    public Countdown countdown;

    [SerializeField] GameObject effectsCamera;

    [Header("Paneles UI")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gamePanel;

    const int MAX_POINTS = 999;
    int _points;

    const int MAX_HEALTH = 10;
    int _health;

    int _killCount;

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
            _health = value;
            if (_health < 0)
            {
                startGameOver();
            }
            else if (_health > MAX_HEALTH)
                _health = MAX_HEALTH;
            UIManager.UpdateHealthText(_health);
        }
    }

    public int KillCount { get => _killCount; set => _killCount = value; }

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
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
            GameManager.Instance.SetScene("Patata");
		}

        if(countdown.getElapsedTime() <= 0)
        {
            countdown.StopTimer();
            startGameOver();
            
        }
    }

    public void startGameOver()
    {
        gameOver = true;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public bool getGameOver()
    {
        return gameOver;
    }

}
