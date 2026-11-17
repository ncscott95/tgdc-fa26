using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoreSteakButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Button _button;
    [SerializeField] private WagerManager _wager;

    private int _cost = 0;

    void Start()
    {
        CalculateCost();
    }

    public void BuySteakSlot()
    {
        PersistentData.Instance.Money -= _cost;
        PersistentData.Instance.GetSelectedLevel().SteakSlots++;
        CalculateCost();
        _wager.UpdateSteakCounter();
        _wager.RecalculateWager();
    }

    private void CalculateCost()
    {
        LevelDetails level = PersistentData.Instance.GetSelectedLevel();

        if (level.SteakSlots < PersistentData.MaxSteakSlots)
        {
            _cost = Prices.Level(level.SteakSlots + level.SteakPriceOffset);
            UpdateButton();
        }
        else
        {
            _button.interactable = false;
            _buttonText.color = Color.gray;
            _buttonText.text = $"max steak";
        }
    }

    private void UpdateButton()
    {
        _button.interactable = _cost <= PersistentData.Instance.Money;
        _buttonText.color = _cost <= PersistentData.Instance.Money ? Color.white : Color.gray;
        _buttonText.text = $"more steak\n${_cost}";
    }
}
