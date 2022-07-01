using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //IMPORTANT
public class GameManager : Singleton<GameManager>
{
    public static event Action OnStartGame;
    public static event Action OnEndGame;

    internal bool isRestart;

    public void StartGame()
    {
        OnStartGame?.Invoke();
    }
    public void EndGame()
    {
        OnEndGame?.Invoke();
    }

}

