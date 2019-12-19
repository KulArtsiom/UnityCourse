using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string nextLevel;

    public static void LoadLevel(string level)
    {
        nextLevel = level;
        SceneManager.LoadScene("Loading");
    }

    private IEnumerator Start()
    {
        GameManager.SetGameState(GameState.Loadind);

        yield return new WaitForSeconds(1f);

        if (string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene("MainMenu");
            yield break;
        }


        var loading = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);

        while (loading.isDone)
        {
            // тут можем получать прогресс 
            //loading.progress
                
                
            //пока ничего не закгрузится откладываем до конца кадра
            yield return null;
        }

        nextLevel = null;
        SceneManager.UnloadSceneAsync("LoadingScene");
    }
}