using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager;
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
                //startGameOver
                //detener al jugador
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

        GameManager.Instance.CurrentLevelManager = this;
    }
}
