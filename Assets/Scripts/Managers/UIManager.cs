using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    public Text scorePoints;
    public Text highScorePoints;

    public void SetScorePoints(int score)
    {
        scorePoints.text = score.ToString();
    }
    public void SetHighScorePoints(int newHighScore)
    {
        highScorePoints.text = newHighScore.ToString();
    }
}
