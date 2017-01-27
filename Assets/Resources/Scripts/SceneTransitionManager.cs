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

    public void GoToShop()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
    }

    public void GoToGame()
    {
        SceneManager.UnloadSceneAsync("Shop");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }
    
}