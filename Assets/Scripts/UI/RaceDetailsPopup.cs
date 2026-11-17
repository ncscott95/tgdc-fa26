using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceDetailsPopup : Popup
{
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _thumbnail;
    [SerializeField] private MedalDisplay _medalDisplay;

    private LevelDetails _selectedLevel;

    public override void Show()
    {
        _selectedLevel = PersistentData.Instance.GetSelectedLevel();
        _description.text = _selectedLevel.Description;
        _thumbnail.sprite = _selectedLevel.Thumbnail;

        List<bool> medalStates = new List<bool>();
        for (int i = 0; i < 3; i++)
        {
            medalStates.Add(PersistentData.Instance.GetSelectedLevel().Details[i].MedalEarned);
        }

        _medalDisplay.SetMedals(medalStates[0], medalStates[1], medalStates[2]);

        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
