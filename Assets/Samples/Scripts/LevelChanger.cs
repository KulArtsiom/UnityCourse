using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public static string levelToLoad;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(string levelIndex, Animator animator)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public AsyncOperation Load(string nextLevel)
    {
        return SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
