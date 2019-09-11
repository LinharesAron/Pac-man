using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dots : MonoBehaviour, IScorePoints
{
    public ScorePoints points;
    public bool isLarge;

    public void Destroy()
    {
        if (isLarge)
            Events.Instance.OnGameModeChanged.Invoke(GameModeManager.GameMode.FRIGHTENED);
        Destroy(gameObject);
    }

    public ScorePoints GetScorePoints()
    {
        return points;
    }
}
