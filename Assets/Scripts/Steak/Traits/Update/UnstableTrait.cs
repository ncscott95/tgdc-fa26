using UnityEngine;

public class UnstableTrait : Trait, IUpdateBehavior
{
    public UnstableTrait() : base(TraitID.Unstable, "Unstable") { }

    public void Invoke(Rigidbody2D rigidbody2D)
    {
        
    }
}
