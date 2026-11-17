using System.Collections;
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
        PersistentData.Instance.SteakSlots++;
        CalculateCost();
        _wager.UpdateSteakCounter();
    }

    private void CalculateCost()
    {
        if (PersistentData.Instance.SteakSlots < PersistentData.MaxSteakSlots)
        {
            _cost = Prices.SteakSlot(PersistentData.Instance.SteakSlots);
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
