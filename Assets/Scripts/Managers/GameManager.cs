using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    int _currentLevel = 1;
    public int CurrentLevel
    {
        get { return _currentLevel; }
    }

    int _currentHighScore;
    int _currentScore = 0;
    public int CurrentScore
    {
        get { return _currentScore; }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _currentHighScore = 0;
    }

    public void IncreaseScore(int p)
    {
        _currentScore += p;
        UIManager.Instance.SetScorePoints(_currentScore);
        if (_currentScore >= _currentHighScore)
        {
            UIManager.Instance.SetHighScorePoints(_currentScore);
            _currentHighScore = _currentScore;
        }
    }

}
