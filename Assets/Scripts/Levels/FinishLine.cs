using UnityEngine;

public class FinishLine : SteakTrigger
{
    private bool _firstCrossed = false;

    public override void OnSteakEnter(Steak steak)
    {
        if (_firstCrossed) return;

        if (PersistentData.Instance.SelectedSteaks.Contains(steak.Data))
        {
            Debug.Log("Player wins!");
        }
        else
        {
            Debug.Log("Player loses");
        }
    }
}
