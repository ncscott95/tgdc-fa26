using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SteakDataDisplay : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private TextMeshProUGUI _descriptionField;
    [SerializeField] private List<TraitCapsule> _traitCapsules = new List<TraitCapsule>();

    private SteakData _selectedData;
    
    public void ShowDisplay(SteakData data)
    {
        if (data == _selectedData) return;

        _selectedData = data;
        _nameField.text = data.Name;
        _descriptionField.text = CreateDescriptionString(data.Descriptors);
        
        for (int i = 0; i < 3; i++)
        {
            if (i <= data.Traits.Count - 1)
            {
                _traitCapsules[i].SetTrait(data.Traits[i]);
            }
            else
            {
                _traitCapsules[i].SetEmpty();
            }
        }

        _popup.Show();
    }

    public void HideDisplay()
    {
        _popup.Hide();
        _selectedData = null;
    }

    public static string CreateDescriptionString(List<string> descriptors)
    {
        // assumes exactly two descriptors
        string output = $"this mis-steak is {descriptors[0]} and {descriptors[1]}.";
        return output.ToLower();
    }
}
