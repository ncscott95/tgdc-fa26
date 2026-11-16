using System.Collections.Generic;
using UnityEngine;

public class RaceEndManager : MonoBehaviour
{
    [SerializeField] private GameObject _boundingBox;
    [SerializeField] private GameObject _winBackground;
    [SerializeField] private GameObject _loseBackground;
    [SerializeField] private MedalDisplay _medalDisplay;

    void Awake()
    {
        _boundingBox.SetActive(false);
        _winBackground.SetActive(false);
        _loseBackground.SetActive(false);
    }

    public void EndRace(bool playerWin)
    {
        if (playerWin)
        {
            // TODO: don't think this will automatically update Levels[i].RaceDetails
            PersistentData.Instance.GetSelectedRace().MedalEarned = true;
            // TODO: apply change in money
        }
        else
        {
            // TODO: apply change in money
        }

        GameObject background = playerWin ? _winBackground : _loseBackground;
        background.SetActive(true);

        // TODO: show change in money (might be nice to have a count up with sound effect?)

        List<bool> medalStates = new List<bool>();
        for (int i = 0; i < 3; i++)
        {
            medalStates.Add(PersistentData.Instance.GetSelectedLevel().Details[i].MedalEarned);
        }

        _medalDisplay.SetMedals(medalStates[0], medalStates[1], medalStates[2]);

        _boundingBox.SetActive(true);
    }
}
