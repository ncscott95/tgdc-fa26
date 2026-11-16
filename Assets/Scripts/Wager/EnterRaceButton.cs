using UnityEngine;

public class EnterRaceButton : MonoBehaviour
{
    [SerializeField] private SteakContainerTrigger _container;

    public void EnterRace()
    {
        if (_container.ContainedSteaks.Count < 1 || _container.ContainedSteaks.Count > PersistentData.Instance.SteakSlots) return;

        foreach (Steak steak in _container.ContainedSteaks)
        {
            PersistentData.Instance.SelectedSteaks.Add(steak.Data);
        }

        SceneLoader.Instance.ChangeScene(PersistentData.Instance.SelectedLevel.Scene);
    }
}
