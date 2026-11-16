using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewSteakButton : MonoBehaviour
{
    [SerializeField] private MeatGenerator steakGenerator;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Button _button;

    private int _cost = 0;

    void Start()
    {
        CalculateCost();
        PersistentData.Instance.OnSteaksChanged += CalculateCost;
    }

    void OnDestroy()
    {
        PersistentData.Instance.OnSteaksChanged -= CalculateCost;        
    }

    public void NewSteak()
    {
        PersistentData.Instance.Money -= _cost;
        steakGenerator.GenerateMeat(true);
    }

    private void CalculateCost()
    {
        if (PersistentData.Instance.Steaks.Count == 0) _cost = 0;
        else _cost = Prices.Level(PersistentData.Instance.Steaks.Count - 1);
        UpdateButton();
    }

    private void UpdateButton()
    {
        _button.interactable = _cost <= PersistentData.Instance.Money;
        _buttonText.color = _cost <= PersistentData.Instance.Money ? Color.white : Color.gray;
        _buttonText.text = $"make a mis-steak: ${_cost}";
    }
}
