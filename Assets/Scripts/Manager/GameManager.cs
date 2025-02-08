using System;
using UnityEngine;

public static class GameManager
{
    public static GameState currentState { get; private set; }

    // GameStates
    public static event Action OnInMenuState;
    public static event Action OnGameOverState;
    public static event Action OnInGameState;
    public static event Action OnCharacterSelection;
    public static event Action OnItemSelection;

    public static event Action OnGameStarted;
    


    [RuntimeInitializeOnLoadMethod]
    private static void InitGame()
    {
        Application.targetFrameRate = 60;
        SetGameState(GameState.InMenu);
    }

    public static void SetGameState(GameState _newState)
    {
        currentState = _newState;

        switch (_newState)
        {
            case GameState.InMenu:
                OnInMenuState?.Invoke();
                break;

            case GameState.ItemSelection: // Pause Time to let the player choose an item
                PauseTime();
                OnItemSelection?.Invoke();
                break;

            case GameState.CharacterSelection:
                OnCharacterSelection?.Invoke();
                break;

            case GameState.InGame: // Ensure that the game is resume after a gameOver or an Item selection
                ResumeTime();
                OnInGameState?.Invoke();
                break;

            case GameState.GameOver:
                PauseTime();
                OnGameOverState?.Invoke();
                break;
        }
    }

    public static void StartGame()
    {
        OnGameStarted?.Invoke();
        SetGameState(GameState.InGame);
    }

    static void PauseTime() => Time.timeScale = 0;
    static void ResumeTime() => Time.timeScale = 1;
}

public enum GameState
{
    InMenu,
    CharacterSelection,
    ItemSelection,
    InGame,
    GameOver
}