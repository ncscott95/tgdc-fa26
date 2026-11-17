using System;
using System.Collections.Generic;

public class PersistentData : Singleton<PersistentData>
{
    public const int MaxSteakSlots = 7;
    private int _money = 10;
    public int Money { get => _money; set => SetMoney(value); }
    public List<LevelDetails> Levels = new List<LevelDetails>();
    public int SelectedLevel;
    public int SelectedRace;
    public List<SteakData> Steaks = new List<SteakData>();
    public List<SteakData> SelectedSteaks = new List<SteakData>();
    public event Action<int> OnMoneyChanged;
    public event Action OnSteaksChanged;
    public int WagerWinAmount;
    public int WagerLoseAmount;

    public void SaveSteak(SteakData steakData)
    {
        Steaks.Add(steakData);
        OnSteaksChanged?.Invoke();
    }

    public void RemoveSteak(SteakData steakData)
    {
        Steaks.Remove(steakData);
        OnSteaksChanged?.Invoke();
    }

    public LevelDetails GetSelectedLevel()
    {
        return Levels[SelectedLevel];
    }

    public RaceDetails GetSelectedRace()
    {
        return Levels[SelectedLevel].Details[SelectedRace];
    }

    private void SetMoney(int money)
    {
        _money = money;
        OnMoneyChanged?.Invoke(money);
    }
}
