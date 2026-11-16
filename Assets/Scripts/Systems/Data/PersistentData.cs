using System.Collections.Generic;
using UnityEngine;

public class PersistentData : Singleton<PersistentData>
{
    public int Money = 10;
    public List<LevelDetails> Levels = new List<LevelDetails>();
    public LevelDetails SelectedLevel;
    public RaceDetails SelectedRace;
    public List<SteakData> Steaks = new List<SteakData>();

    public void SaveSteak(SteakData steakData)
    {
        Steaks.Add(steakData);
    }

    public void RemoveSteak(SteakData steakData)
    {
        Steaks.Remove(steakData);
    }
}
