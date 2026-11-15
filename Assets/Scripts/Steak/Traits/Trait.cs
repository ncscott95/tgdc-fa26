using UnityEngine;

public enum TraitID
{
    Sticky,
    Slippery,
    Bouncy,
    Wriggly,
    Unstable,
    Jumpy,
    Tiny,
    Giant,
    Heavy,
    Light
}

public abstract class Trait
{
    public TraitID ID { get; private set; }
    public string Name { get; private set; }

    public Trait(TraitID id, string name)
    {
        ID = id;
        Name = name;
    }
}
