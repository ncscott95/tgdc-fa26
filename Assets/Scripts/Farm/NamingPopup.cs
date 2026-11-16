using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NamingPopup : MonoBehaviour
{
    private const int MaxNameLength = 8;
    [SerializeField] private Popup _popup;
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private NewSteakButton _newSteakButton;
    [SerializeField] private TextMeshProUGUI _descriptionField;
    [SerializeField] private List<TraitCapsule> _traitCapsules = new List<TraitCapsule>();
    [SerializeField] private Button _okButton;

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

        OnNameFieldUpdate();
    }

    public void OnNameFieldUpdate()
    {
        int nameLength = _nameField.text.Length;
        _okButton.interactable = nameLength > 0 & nameLength <= MaxNameLength;
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
