using UnityEngine;

public interface ISizeBehavior
{
    public float RadiusMultiplier { get; }
    public void Invoke(SteakData steakData);
}
