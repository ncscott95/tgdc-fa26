using UnityEngine;

public class BouncyTrait : Trait, IPhysicsMaterialBehavior
{
    public BouncyTrait() : base(TraitID.Bouncy, "Bouncy", new Color(0.7886792f, 0.4070357f, 0.3616019f)) { }

    public void Invoke(PhysicsMaterial2D material)
    {
        material.bounciness = 1f;
        material.bounceCombine = PhysicsMaterialCombine2D.Maximum;
    }
}
