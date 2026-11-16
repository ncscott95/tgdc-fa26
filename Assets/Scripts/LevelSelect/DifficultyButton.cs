using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private int index;

    public void SelectDifficulty()
    {
        PersistentData.Instance.SelectedRace = index;
    }
}
