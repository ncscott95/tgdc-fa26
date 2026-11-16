using UnityEngine;

public class SteakCombiner : MonoBehaviour
{
    [SerializeField] private SteakContainerTrigger _containerLeft;
    [SerializeField] private SteakContainerTrigger _containerRight;
    [SerializeField] private MeatGenerator steakGenerator;

    public void OnPushButton()
    {
        Steak[] leftSteak = new Steak[_containerLeft.ContainedSteaks.Count];
        Steak[] rightSteak = new Steak[_containerRight.ContainedSteaks.Count];
        _containerLeft.ContainedSteaks.CopyTo(leftSteak);
        _containerRight.ContainedSteaks.CopyTo(rightSteak);
        
        if (leftSteak.Length != 1 || rightSteak.Length != 1)
        {
            // TODO: play fail SFX?
            return;
        }

        // TODO: remove steaks from player data

        Destroy(leftSteak[0].gameObject);
        Destroy(rightSteak[0].gameObject);

        SteakData combinedData = CombineSteakData(leftSteak[0].Data, rightSteak[0].Data);
        steakGenerator.GenerateMeat(combinedData);

        // TODO: play particles
    }

    private SteakData CombineSteakData(SteakData left, SteakData right)
    {
        SteakData combined = new SteakData();

        return combined;
    }
}
