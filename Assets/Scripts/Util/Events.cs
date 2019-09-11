using UnityEngine;
using UnityEngine.Events;

public class Events : Singleton<Events>
{
    public EventChangeMode OnGameModeChanged;
    [System.Serializable] public class EventChangeMode : UnityEvent<GameModeManager.GameMode> { }
}
