using System;
using UnityEngine;

public static class GameManager
{
    public static GameState currentState { get; private set; } = GameState.InMenu;

    public static event Action OnInMenuState;
    public static event Action OnInGameState;
    public static event Action OnGameOverState;


    [RuntimeInitializeOnLoadMethod]
    private static void InitGame()
    {
        Application.targetFrameRate = 60;
    }
    public static void SetGameState(GameState _newState)
    {
        currentState = _newState;

        switch (_newState)
        {
            case GameState.InMenu:
                OnInMenuState?.Invoke();
                break;
            case GameState.InGame:
                OnInGameState?.Invoke();
                break;
            case GameState.GameOver:
                OnGameOverState?.Invoke();
                break;
        }
    }
}

public enum GameState
{
    InMenu,
    InGame,
    GameOver
}