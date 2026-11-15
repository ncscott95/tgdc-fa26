using UnityEngine;

public class HeavyTrait : Trait, IMassBehavior
{
    public float MassMultiplier => 4f;
    public HeavyTrait() : base(TraitID.Heavy, "Heavy") { }
}
