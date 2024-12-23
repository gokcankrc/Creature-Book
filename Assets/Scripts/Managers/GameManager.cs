using System;
using Ky;
using UnityEngine;
using Logger = Ky.Logger;

public class GameManager : Singleton<GameManager>
{
    public static State gameState = State.Waiting;
    public static Action gameInitialized;

    private void Start()
    {
        gameInitialized?.Invoke();
    }

    public void ChangeState(State newState)
    {
        Ky.Logger.Log($"State: <color=white>{gameState.ToString()}</color> => <color=white>{newState.ToString()}</color>", Logger.DomainType.System);
        gameState = newState;
    }

    public enum State
    {
        Waiting,
        InCombat
    }
}