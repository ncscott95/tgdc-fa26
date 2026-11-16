using UnityEngine;

public class FinishLine : SteakTrigger
{
    [SerializeField] private RaceEndManager _endScreen;
    private bool _firstCrossed = false;

    public override void OnSteakEnter(Steak steak)
    {
        if (_firstCrossed) return;

        _firstCrossed = true;

        bool playerWin = PersistentData.Instance.SelectedSteaks.Contains(steak.Data);
        _endScreen.EndRace(playerWin);
    }
}
