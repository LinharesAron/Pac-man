using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameModeManager : MonoBehaviour
{
    public enum GameMode { CHASE, SCATTER, FRIGHTENED };

    public GameMode _currentGameMode;

    private int index;
    private float time = 0;
    private float updateRate = 1f / 60f;
    private float[] waitTime;

    private void Start()
    {
        InvokeRepeating("GameCycle", 0.0f, updateRate);
        waitTime = new float[] { 7f, 20f };
        Events.Instance.OnGameModeChanged.AddListener(HandleGameStateChanged);
        Events.Instance.OnGameModeChanged.Invoke(GameMode.SCATTER);
    }

    private void GameCycle()
    {
        if (_currentGameMode == GameMode.FRIGHTENED)
            return;

        time += updateRate;
        if (time > waitTime[index % waitTime.Length])
        {
            time = 0;
            index++;
            SwitchMode();
        }
    }

    private void HandleGameStateChanged(GameMode newMode)
    {
        _currentGameMode = newMode;
    }

    private void SwitchMode()
    {
        GameMode newMode = GameMode.SCATTER;
        if (_currentGameMode == GameMode.SCATTER)
            newMode = GameMode.CHASE;

        Events.Instance.OnGameModeChanged.Invoke(newMode);
    }
}
