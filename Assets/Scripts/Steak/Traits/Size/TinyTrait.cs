using UnityEngine;

public class TinyTrait : Trait, ISizeBehavior
{
    public float RadiusMultiplier => 0.75f;
    public TinyTrait() : base(TraitID.Tiny, "Tiny", new Color(0.9471698f, 0.6028528f, 0.3699323f)) { }

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
