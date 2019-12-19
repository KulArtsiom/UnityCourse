using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Loadind,
    Game,
    GamePause
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetGameState(GameState.Game);
        print(Player);
        print($"Enemies count {Enemies.Count}");
        
        
//        Debug.Log("Start");
//        StartCoroutine(Wait());
    }

     
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Hello world");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
