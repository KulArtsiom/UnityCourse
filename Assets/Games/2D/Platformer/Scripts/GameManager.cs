using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Loading,
    Game,
    GamePause,
}

public class GameManager : MonoBehaviour
{
    private static GameState currentGameState;
    public static Action<GameState> GameStateAction;

    public static GameState GetGameState => currentGameState;

    public IPlayer Player;
    public List<IEnemy> Enemies = new List<IEnemy>();

    public static void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        GameStateAction?.Invoke(newGameState);
    }

    private void Start()
    {
        SetGameState(GameState.Game);
        print(Player);
        print($"Enemies count {Enemies.Count}");
    }
    
}
