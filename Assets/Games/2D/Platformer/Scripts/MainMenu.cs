using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    private void Start()
    {
        GameManager.SetGameState(GameState.MainMenu);
    }

    public void LoadLevel(string level)
    {
        SceneLoader.LoadLevel(level);
    }
}
