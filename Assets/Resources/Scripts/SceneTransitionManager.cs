using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    public void StartGame()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    public void GoToScore()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Score", LoadSceneMode.Additive);
    }

    public void GoToGame()
    {
        SceneManager.UnloadSceneAsync("Score");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }
    
}