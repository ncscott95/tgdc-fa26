using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Farm,
    LevelSelect
}

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private Transition transition;
    private List<SceneType> loadedScenes = new List<SceneType>();
    private Coroutine changeSceneRoutine;

    public void AddScene(SceneType sceneType)
    {
        SceneManager.LoadScene(sceneType.ToString(), LoadSceneMode.Additive);
        loadedScenes.Add(sceneType);
    }

    public void UnloadScene(SceneType sceneType)
    {
        StartCoroutine(UnloadSceneRoutine(sceneType));
    }

    public void ChangeScene(SceneType newScene)
    {
        if (changeSceneRoutine != null) StopCoroutine(changeSceneRoutine);
        changeSceneRoutine = StartCoroutine(ChangeSceneRoutine(newScene));
    }

    public IEnumerator ChangeSceneRoutine(SceneType newScene)
    {
        yield return transition.FadeIn();

        foreach (SceneType sceneType in loadedScenes)
        {
            UnloadScene(sceneType);
        }

        AddScene(newScene);

        yield return transition.FadeOut();
    }

    private IEnumerator UnloadSceneRoutine(SceneType sceneType)
    {
        yield return SceneManager.UnloadSceneAsync(sceneType.ToString());
        loadedScenes.Remove(sceneType);
    }
}
