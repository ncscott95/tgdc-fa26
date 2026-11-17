using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelDetails
{
    public string Race = "";
    public string Description = "";
    public Sprite Thumbnail;
    public bool Locked = true;
    public int UnlockPrice = 0;
    public List<RaceDetails> Details = new List<RaceDetails>();
    public SceneType Scene;
}
