using UnityEngine;

public class StickyTrait : Trait, IPhysicsMaterialBehavior
{
    public StickyTrait() : base(TraitID.Sticky, "Sticky") { }

    public void Invoke(PhysicsMaterial2D material)
    {
        material.friction += 0.5f;
    }
}
