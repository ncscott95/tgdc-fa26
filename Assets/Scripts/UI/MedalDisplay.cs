using UnityEngine;
using UnityEngine.UI;

public class MedalDisplay : MonoBehaviour
{
    [SerializeField] private Sprite _bronzeLocked;
    [SerializeField] private Sprite _silverLocked;
    [SerializeField] private Sprite _goldLocked;
    [SerializeField] private Sprite _bronzeUnlocked;
    [SerializeField] private Sprite _silverUnlocked;
    [SerializeField] private Sprite _goldUnlocked;
    [SerializeField] private Image _bronzeSlot;
    [SerializeField] private Image _silverSlot;
    [SerializeField] private Image _goldSlot;

    public void SetMedals(bool bronze, bool silver, bool gold)
    {
        _bronzeSlot.sprite = bronze ? _bronzeUnlocked : _bronzeLocked;
        _silverSlot.sprite = silver ? _silverUnlocked : _silverLocked;
        _goldSlot.sprite = gold ? _goldUnlocked : _goldLocked;
    }
}
