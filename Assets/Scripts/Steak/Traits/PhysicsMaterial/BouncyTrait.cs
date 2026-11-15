using UnityEngine;

public class BouncyTrait : Trait, IPhysicsMaterialBehavior
{
    public BouncyTrait() : base(TraitID.Bouncy, "Bouncy") { }

    public void Invoke(PhysicsMaterial2D material)
    {
        material.bounciness = 1f;
        material.bounceCombine = PhysicsMaterialCombine2D.Maximum;
    }
}
