using UnityEngine;

public class StickyTrait : Trait, IPhysicsMaterialBehavior
{
    public StickyTrait() : base(TraitID.Sticky, "Sticky", new Color(0.7215686f, 0.4431373f, 0.5843138f)) { }

    public void Invoke(PhysicsMaterial2D material)
    {
        material.friction += 0.5f;
    }
}
