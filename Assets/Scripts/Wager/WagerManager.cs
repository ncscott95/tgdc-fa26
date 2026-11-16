using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WagerManager : MonoBehaviour
{
    [SerializeField] private Button _increaseWagerButton;
    [SerializeField] private Button _decreaseWagerButton;
    [SerializeField] private TextMeshProUGUI _wagerText;
    [SerializeField] private Button _moreSteakButton;
    [SerializeField] private TextMeshProUGUI _moreSteakText;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private TextMeshProUGUI _loseText;
    [SerializeField] private Button _raceButton;

    private int _wagerLevel;

    void Start()
    {
        PersistentData.Instance.SelectedSteaks.Clear();
        _wagerLevel = 0;

        while (Prices.Level(_wagerLevel) < PersistentData.Instance.Money)
        {
            _wagerLevel++;
        }

        _wagerLevel--;
        UpdateWager();
    }

    public void IncreaseWager()
    {
        _wagerLevel++;
        UpdateWager();
    }

    public void DecreaseWager()
    {
        _wagerLevel--;
        UpdateWager();
    }

    private void UpdateWager()
    {
        int wagerCost = 0;
        int maxWager = PersistentData.Instance.GetSelectedRace().MaxWager;
        int nextWagerCost = Prices.Level(_wagerLevel + 1);

        if (_wagerLevel == -1)
        {
            _decreaseWagerButton.interactable = false;
        }
        else
        {
            wagerCost = Prices.Level(_wagerLevel);
            _decreaseWagerButton.interactable = true;
        }

        _increaseWagerButton.interactable = nextWagerCost <= maxWager && PersistentData.Instance.Money >= nextWagerCost;
        _wagerText.text = $"${wagerCost}";

        UpdateRewards(wagerCost);
    }

    private void UpdateRewards(int wagerCost)
    {
        int money = PersistentData.Instance.Money;
        int numOpponents = PersistentData.Instance.GetSelectedRace().NumOpponents;
        int winAmount = wagerCost * (numOpponents + 2);
        int loseAmount = money - wagerCost;
        if (wagerCost == 0) winAmount = 15;

        _winText.text = $"{money} -> <color=#008000ff>{winAmount}</color>";
        _loseText.text = $"{money} -> <color=red>{loseAmount}</color>";
    }

    public int GetWagerAmount()
    {
        return Prices.Level(_wagerLevel);
    }
}
