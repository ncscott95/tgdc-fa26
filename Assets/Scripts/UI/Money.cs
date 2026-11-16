using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    
    void Start()
    {
        PersistentData.Instance.OnMoneyChanged += UpdateMoney;
        UpdateMoney(PersistentData.Instance.Money);
    }

    void OnDestroy()
    {
        PersistentData.Instance.OnMoneyChanged -= UpdateMoney;        
    }

    private void UpdateMoney(int money)
    {
        _moneyText.text = $"${money}";
    }
}
