using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private bool _startSelected;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private DifficultyButton _otherDifficulty1;
    [SerializeField] private DifficultyButton _otherDifficulty2;
    private bool _savedColors = false;
    private ColorBlock _defaultColors;
    private Color _unselectedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);

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
        if (!_savedColors)
        {
            _defaultColors = _button.colors;
            _savedColors = true;
        }

        _button.colors = _defaultColors;
        _text.color = Color.white;
        _otherDifficulty1.SetUnselected();
        _otherDifficulty2.SetUnselected();
    }

    public void SetUnselected()
    {
        if (!_savedColors)
        {
            _defaultColors = _button.colors;
            _savedColors = true;
        }

        ColorBlock cb = new ColorBlock
        {
            selectedColor = _unselectedColor,
            highlightedColor = _unselectedColor,
            normalColor = _unselectedColor,
            pressedColor = _unselectedColor,
            disabledColor = _unselectedColor,
            colorMultiplier = 1.0f,
            fadeDuration = 0.1f
        };
        _button.colors = cb;
        _text.color = _unselectedColor;
    }
}
