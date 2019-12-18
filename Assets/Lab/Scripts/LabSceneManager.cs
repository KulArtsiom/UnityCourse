using UnityEngine;
using UnityEngine.SceneManagement;

    public class LabSceneManager : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
