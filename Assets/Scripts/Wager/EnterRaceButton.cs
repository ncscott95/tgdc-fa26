using UnityEngine;

public class EnterRaceButton : MonoBehaviour
{
    [SerializeField] private SteakContainerTrigger _container;
    [SerializeField] private WagerManager _wager;

    public void EnterRace()
    {
        if (_container.ContainedSteaks.Count < 1 || 
            _container.ContainedSteaks.Count > PersistentData.Instance.GetSelectedLevel().SteakSlots ||
            _container.ContainedSteaks.Count > PersistentData.Instance.GetSelectedRace().NumOpponents) return;

        PersistentData.Instance.SelectedSteaks.Clear();

        foreach (Steak steak in _container.ContainedSteaks)
        {
            PersistentData.Instance.SelectedSteaks.Add(steak.Data);
        }

        SceneLoader.Instance.ChangeScene(PersistentData.Instance.GetSelectedLevel().Scene);
    }
}
