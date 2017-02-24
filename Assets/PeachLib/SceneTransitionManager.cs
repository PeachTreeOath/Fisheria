using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This scene manager handles scene loading when there is a persistent scene used.
/// A temporary scene swap is needed because you can't load scenes additively and
/// set active scene on the same frame, so a buffer scene must be used and then
/// merged after frame 1.
/// </summary>
public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    private Scene tempScene;

    public void StartGame()
    {
        string nextSceneName = "Game";

        SceneManager.UnloadSceneAsync("Title");

        tempScene = SceneManager.CreateScene("TempScene");
        SceneManager.SetActiveScene(tempScene);

        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);

        StartCoroutine(SetActive(SceneManager.GetSceneByName(nextSceneName)));
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

    public IEnumerator SetActive(Scene scene)
    {
        int i = 0;
        while (i == 0)
        {
            i++;
            yield return null;
        }
        SceneManager.SetActiveScene(scene);
        SceneManager.MergeScenes(tempScene, scene);

        yield break;
    }
}