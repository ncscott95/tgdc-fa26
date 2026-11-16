using System.Collections.Generic;
using UnityEngine;

public class SteakContainerTrigger : SteakTrigger
{
    public List<Steak> ContainedSteaks { get; private set; } = new List<Steak>();

    public override void OnSteakEnter(Steak steak)
    {
        if (!ContainedSteaks.Contains(steak))
        {
            ContainedSteaks.Add(steak);
        }
    }

    public override void OnSteakExit(Steak steak)
    {
        if (ContainedSteaks.Contains(steak))
        {
            ContainedSteaks.Remove(steak);
        }
    }
}
