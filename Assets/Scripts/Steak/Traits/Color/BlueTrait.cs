using UnityEngine;

public class BlueTrait : Trait, IColorBehavior
{
    public Color SpriteColor => new Color(0.42f, 0.57f, 1f, 1f);
    public BlueTrait() : base(TraitID.Blue, "Blue", new Color(0.4842933f, 0.5186109f, 0.8943396f)) { }
}
