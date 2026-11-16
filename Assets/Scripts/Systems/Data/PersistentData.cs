using System.Collections.Generic;
using UnityEngine;

public class PersistentData : Singleton<PersistentData>
{
    public int Money = 10;
    public List<LevelDetails> Levels = new List<LevelDetails>();
    public int SelectedLevel;
    public int SelectedRace;
    public List<SteakData> Steaks = new List<SteakData>();
    public List<SteakData> SelectedSteaks = new List<SteakData>();
    public int SteakSlots = 1;

    public void SaveSteak(SteakData steakData)
    {
        Steaks.Add(steakData);
    }

    public void RemoveSteak(SteakData steakData)
    {
        Steaks.Remove(steakData);
    }

    public LevelDetails GetSelectedLevel()
    {
        return Levels[SelectedLevel];
    }

    public RaceDetails GetSelectedRace()
    {
        return Levels[SelectedLevel].Details[SelectedRace];
    }
}
