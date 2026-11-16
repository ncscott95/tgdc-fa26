using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private int index;

    public void SelectDifficulty()
    {
        Debug.Log($"Selecting difficulty {index}");
        PersistentData.Instance.SelectedRace = PersistentData.Instance.SelectedLevel.Details[index];
    }
}
