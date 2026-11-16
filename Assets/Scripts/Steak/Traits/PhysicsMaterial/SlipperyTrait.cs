using UnityEngine;

public class SlipperyTrait : Trait, IPhysicsMaterialBehavior
{
    public SlipperyTrait() : base(TraitID.Slippery, "Slippery", new Color(0.9019608f, 0.764706f, 0.1647059f)) { }

    public void Invoke(PhysicsMaterial2D material)
    {
        material.friction -= 0.5f;
    }
}
