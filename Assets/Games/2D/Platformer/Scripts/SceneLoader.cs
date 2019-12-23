using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{
    private static string nextLevel;
    private Quaternion quaternion;
    private Animator animator;
    
    public Text text;
    public Animator Animator;

    private string LoadingText;
    private int frame;
    private LevelChanger LevelChanger;

    public static void LoadLevel(string level)
    {
        nextLevel = level;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator Start()
    {
        quaternion = Quaternion.AngleAxis(3, Time.deltaTime * 0.5f * Vector3.up);
        text.text = "Loading";
        LevelChanger = gameObject.AddComponent<LevelChanger>();

        GameManager.SetGameState(GameState.Loading);
        yield return new WaitForSeconds(3f);
        if (string.IsNullOrEmpty(nextLevel))
        {
            LevelChanger.FadeToLevel("MainMenu", Animator);
            yield break;
        }

        var loading = LevelChanger.Load(nextLevel);

        while (!loading.isDone)
        {
            yield return null;
        }

        nextLevel = null;
        SceneManager.UnloadSceneAsync("LoadingScene");
    }

    private void UpdateLoadingText()
    {
        if (text.text.Equals("Loading..."))
        {
            text.text = "Loading";
        }

        text.text += ".";
    }

    private void Update()
    {
        if (frame == 40)
        {
            frame = 0;
            UpdateLoadingText();
        }

        transform.rotation *= quaternion;
        frame++;
    }
}