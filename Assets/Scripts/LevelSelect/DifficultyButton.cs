using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private bool _startSelected;
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _selectedButtonSprite;
    [SerializeField] private Sprite _unselectedButtonSprite;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private DifficultyButton _otherDifficulty1;
    [SerializeField] private DifficultyButton _otherDifficulty2;

    void OnEnable()
    {
        if (_startSelected)
        {
            SelectDifficulty();
        }
    }

    public void SelectDifficulty()
    {
        PersistentData.Instance.SelectedRace = index;
        SetSelected();
    }

    public void SetSelected()
    {
        _button.image.sprite = _selectedButtonSprite;
        _button.image.pixelsPerUnitMultiplier = 3;
        _text.color = Color.black;
        _otherDifficulty1.SetUnselected();
        _otherDifficulty2.SetUnselected();
    }

    public void SetUnselected()
    {
        _button.image.sprite = _unselectedButtonSprite;
        _button.image.pixelsPerUnitMultiplier = 1;
        _text.color = Color.white;
    }
}
