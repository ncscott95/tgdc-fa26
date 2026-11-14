using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Farm
}

public class SceneLoader : Singleton<SceneLoader>
{
    private List<SceneType> loadedScenes = new List<SceneType>();

    public void AddScene(SceneType sceneType)
    {
        SceneManager.LoadScene(sceneType.ToString(), LoadSceneMode.Additive);
        loadedScenes.Add(sceneType);
    }

    public void UnloadScene(SceneType sceneType)
    {
        StartCoroutine(UnloadSceneRoutine(sceneType));
    }

    public void LoadMainMenu()
    {
        foreach (SceneType sceneType in loadedScenes)
        {
            UnloadScene(sceneType);
        }
    }

    private IEnumerator UnloadSceneRoutine(SceneType sceneType)
    {
        yield return SceneManager.UnloadSceneAsync(sceneType.ToString());
        loadedScenes.Remove(sceneType);
    }
}
