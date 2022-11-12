using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager;
    const int MAX_POINTS = 999;
    int _points;

	public int Points { get => _points; set {
            _points = value;
            if (_points < 0)
                _points = 0;
            else if (_points > MAX_POINTS)
                _points = MAX_POINTS;
            UIManager.UpdatePointsText(_points);
        }
    }

	// Start is called before the first frame update
	void Start()
    {
        GameManager.Instance.CurrentLevelManager = this;
    }
}
