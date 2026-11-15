using UnityEngine;

public class SlipperyTrait : Trait, IPhysicsMaterialBehavior
{
    public SlipperyTrait() : base(TraitID.Slippery, "Slippery") { }

    public void Invoke(PhysicsMaterial2D material)
    {
        material.friction -= 0.5f;
    }
}
