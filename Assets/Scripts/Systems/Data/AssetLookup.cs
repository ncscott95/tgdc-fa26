using System.Collections.Generic;
using UnityEngine;

public class AssetLookup : Singleton<AssetLookup>
{
    [SerializeField] public Dictionary<TraitID, Sprite> TraitSpriteTable;
}
