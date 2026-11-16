using System.Collections.Generic;
using UnityEngine;

public class SteakCombiner : MonoBehaviour
{
    [SerializeField] private SteakContainerTrigger _containerLeft;
    [SerializeField] private SteakContainerTrigger _containerRight;
    [SerializeField] private MeatGenerator steakGenerator;

    public void OnPushButton()
    {
        if (_containerLeft.ContainedSteaks.Count != 1 || _containerRight.ContainedSteaks.Count != 1)
        {
            // TODO: play fail SFX?
            return;
        }

        Steak leftSteak = _containerLeft.ContainedSteaks[0];
        Steak rightSteak = _containerRight.ContainedSteaks[0];

        SteakData combinedData = CombineSteakData(leftSteak.Data, rightSteak.Data);

        // TODO: remove steaks from player data
        PersistentData.Instance.RemoveSteak(leftSteak.Data);
        PersistentData.Instance.RemoveSteak(rightSteak.Data);
        Destroy(leftSteak.gameObject);
        Destroy(rightSteak.gameObject);

        steakGenerator.GenerateMeat(combinedData, true);

        // TODO: play particles
    }

    private SteakData CombineSteakData(SteakData left, SteakData right)
    {
        List<Trait> combinedTraits = new List<Trait>();
        combinedTraits.AddRange(left.Traits);
        combinedTraits.AddRange(right.Traits);
        while (combinedTraits.Count > SteakData.MaxTraits)
        {
            combinedTraits.Remove(combinedTraits[Random.Range(0, combinedTraits.Count)]);
        }

        // TODO: generate fancy new exterior node positions
        List<Vector2> nodes = Random.value > 0.5f ? left.ExteriorNodePositions : right.ExteriorNodePositions;

        // TODO: add logic to prevent eyes from overlapping
        List<PosRotScale> eyes = new List<PosRotScale>();
        eyes.AddRange(left.EyeTransforms);
        eyes.AddRange(right.EyeTransforms);

        Vector2 uvOffset = Random.value > 0.5f ? left.TextureUVOffset : right.TextureUVOffset;
        List<string> descriptors = new List<string>()
        {
            Random.value > 0.5f ? left.Descriptors[0] : right.Descriptors[0],
            Random.value > 0.5f ? left.Descriptors[1] : right.Descriptors[1]
        };

        SteakData combined = new SteakData(combinedTraits, nodes, eyes, uvOffset, "", descriptors);
        return combined;
    }
}
