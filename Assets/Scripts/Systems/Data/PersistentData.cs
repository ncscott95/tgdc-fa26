using System.Collections.Generic;

public class PersistentData : Singleton<PersistentData>
{
    public int Money = 10;
    public List<LevelDetails> Levels = new List<LevelDetails>();
    public LevelDetails SelectedLevel;
    public RaceDetails SelectedRace;
}
