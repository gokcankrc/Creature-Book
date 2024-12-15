using System;
using Ky;

public class GameManager : Singleton<GameManager>
{
    private Action gameInitialized;

    private void Start()
    {
        gameInitialized?.Invoke();
    }
}