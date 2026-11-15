using UnityEngine;

public class GiantTrait : Trait, ISizeBehavior
{
    public float RadiusMultiplier => 1.33f;
    public GiantTrait() : base(TraitID.Giant, "Giant") { }

    public void Invoke(SteakData steakData)
    {
        for (int i = 0; i < steakData.ExteriorNodePositions.Count; i++)
        {
            Vector2 position = steakData.ExteriorNodePositions[i];
            position *= RadiusMultiplier;
            steakData.ExteriorNodePositions[i] = position;
        }

        for (int i = 0; i < steakData.EyeTransforms.Count; i++)
        {
            PosRotScale eyeTransform = steakData.EyeTransforms[i];
            eyeTransform.Position *= RadiusMultiplier;
            eyeTransform.Scale *= RadiusMultiplier;
            steakData.EyeTransforms[i] = eyeTransform;
        }
    }
}
