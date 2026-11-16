using UnityEngine;

public class HeavyTrait : Trait, IMassBehavior
{
    public float MassMultiplier => 4f;
    public HeavyTrait() : base(TraitID.Heavy, "Heavy", new Color(0.5292721f, 0.5090423f, 0.6830188f)) { }
}
