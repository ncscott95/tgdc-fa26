using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NamingPopup : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private NewSteakButton _newSteakButton;
    [SerializeField] private TextMeshProUGUI _descriptionField;
    [SerializeField] private List<TraitCapsule> _traitCapsules = new List<TraitCapsule>();

    private SteakData _latestData;

    void OnEnable()
    {
        _latestData = _newSteakButton.GetLatestSteakData();
        _descriptionField.text = SteakDataDisplay.CreateDescriptionString(_latestData.Descriptors);
        
        for (int i = 0; i < 3; i++)
        {
            if (i <= _latestData.Traits.Count - 1)
            {
                _traitCapsules[i].SetTrait(_latestData.Traits[i]);
            }
            else
            {
                _traitCapsules[i].SetEmpty();
            }
        }
    }

    public void ClosePopup()
    {
        _popup.Hide();

        _latestData.Name = _nameField.text;
        _newSteakButton.SetLatestSteakData(_latestData);

        _newSteakButton.OnPopupClose();
        _nameField.text = "";
    }
}
