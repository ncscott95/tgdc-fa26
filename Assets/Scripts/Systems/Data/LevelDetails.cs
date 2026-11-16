using System;
using System.Collections.Generic;

[Serializable]
public class LevelDetails
{
    public string Race = "";
    public bool Locked = true;
    public int UnlockPrice = 0;
    public List<RaceDetails> Details = new List<RaceDetails>();
    public SceneType Scene;
}
