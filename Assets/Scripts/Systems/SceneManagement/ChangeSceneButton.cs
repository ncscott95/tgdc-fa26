using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] private SceneType sceneType;

    public void ChangeScene()
    {
        SceneLoader.Instance.ChangeScene(sceneType);
    }
}
