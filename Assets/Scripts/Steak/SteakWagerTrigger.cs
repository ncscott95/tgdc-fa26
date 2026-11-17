using UnityEngine;

public class SteakWagerTrigger : SteakContainerTrigger
{
    [SerializeField] private WagerManager _wager;
    
    public override void OnSteakEnter(Steak steak)
    {
        if (!ContainedSteaks.Contains(steak))
        {
            ContainedSteaks.Add(steak);
            _wager.AddSteak();
        }
    }

    public override void OnSteakExit(Steak steak)
    {
        if (ContainedSteaks.Contains(steak))
        {
            ContainedSteaks.Remove(steak);
            _wager.RemoveSteak();
        }
    }
}
