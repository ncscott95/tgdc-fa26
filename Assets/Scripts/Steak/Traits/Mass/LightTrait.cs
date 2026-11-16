using UnityEngine;

public class LightTrait : Trait, IMassBehavior
{
    public float MassMultiplier => 0.25f;
    public LightTrait() : base(TraitID.Light, "Light", new Color(0.8872249f, 0.7150943f, 1f)) { }
}
