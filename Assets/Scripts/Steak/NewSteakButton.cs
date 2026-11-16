using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewSteakButton : MonoBehaviour
{
    [SerializeField] private SteakGenerator _steakGenerator;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Button _button;
    [SerializeField] private Popup _namingPopup;
    [SerializeField] private FarmInteractionSystem _interactions;

    private int _cost = 0;
    private Steak _latestSteak = null;

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
        _latestSteak = _steakGenerator.GenerateMeat(true);
        _button.interactable = false;
        _buttonText.text = "making your mis-steak...";
        _interactions.SetDisabled(true);
        StartCoroutine(NamingPopupCoroutine());
    }

    public void NewCombinedSteak(SteakData combinedData)
    {
        _latestSteak = _steakGenerator.GenerateMeat(combinedData, true);
        _button.interactable = false;
        _buttonText.text = "merging your mis-steaks...";
        _interactions.SetDisabled(true);
        StartCoroutine(NamingPopupCoroutine());
    }

    private IEnumerator NamingPopupCoroutine()
    {
        yield return new WaitForSeconds(3);
        _namingPopup.Show();
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

    public void OnPopupClose()
    {
        CalculateCost();
        _interactions.SetDisabled(false);
    }

    public SteakData GetLatestSteakData()
    {
        return _latestSteak.Data;
    }

    public void SetLatestSteakData(SteakData data)
    {
        _latestSteak.SetData(data);
    }
}
