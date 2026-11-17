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
    Light,
    Blue
}

public abstract class Trait
{
    public TraitID ID { get; private set; }
    public string Name { get; private set; }
    public Color Color { get; private set; }

    public Trait(TraitID id, string name, Color color)
    {
        ID = id;
        Name = name;
        Color = color;
    }
}
