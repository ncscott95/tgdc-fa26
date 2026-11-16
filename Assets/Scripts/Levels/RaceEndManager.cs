using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceEndManager : MonoBehaviour
{
    [SerializeField] private GameObject _boundingBox;
    [SerializeField] private GameObject _winBackground;
    [SerializeField] private GameObject _loseBackground;
    [SerializeField] private MedalDisplay _medalDisplay;
    [SerializeField] private TextMeshProUGUI _oldAmount;
    [SerializeField] private TextMeshProUGUI _newAmount;

    void Awake()
    {
        _boundingBox.SetActive(false);
        _winBackground.SetActive(false);
        _loseBackground.SetActive(false);
    }

    public void EndRace(bool playerWin)
    {
        GameObject background = playerWin ? _winBackground : _loseBackground;
        background.SetActive(true);
        
        RaceDetails race = PersistentData.Instance.GetSelectedRace();

        int outcome = PersistentData.Instance.Money;
        string color = "";
        if (playerWin)
        {
            outcome += PersistentData.Instance.CurrentWager * (race.NumOpponents + 2);
            color = "#008000ff";
        }
        else
        {
            outcome -= PersistentData.Instance.CurrentWager;
            color = "#ff0000ff";
        }

        _oldAmount.text = $"{PersistentData.Instance.Money}";
        _newAmount.text = $"<color={color}>{outcome}</color>";

        // TODO: show change in money (might be nice to have a count up with sound effect?)
        PersistentData.Instance.Money = outcome;

        race.MedalEarned = race.MedalEarned || playerWin;

        List<bool> medalStates = new List<bool>();
        for (int i = 0; i < 3; i++)
        {
            medalStates.Add(PersistentData.Instance.GetSelectedLevel().Details[i].MedalEarned);
        }

        _medalDisplay.SetMedals(medalStates[0], medalStates[1], medalStates[2]);

        _boundingBox.SetActive(true);
    }
}
