using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitCapsule : MonoBehaviour
{
    [SerializeField] private Image _capsule;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;

    public void SetTrait(Trait trait)
    {
        _capsule.color = trait.Color;
        _icon.sprite = AssetLookup.Instance.TraitSpriteTable[trait.ID];
        _text.text = trait.Name.ToLower();

        _icon.gameObject.SetActive(true);
        _text.gameObject.SetActive(true);
    }

    public void SetEmpty()
    {
        _capsule.color = Color.white;
        _icon.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
    }
}
