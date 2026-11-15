using UnityEngine;

public class LightTrait : Trait, IMassBehavior
{
    public float MassMultiplier => 0.25f;
    public LightTrait() : base(TraitID.Light, "Light") { }
}
