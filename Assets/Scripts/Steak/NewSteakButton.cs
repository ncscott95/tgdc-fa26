using UnityEngine;

public class NewSteakButton : MonoBehaviour
{
    [SerializeField] private MeatGenerator steakGenerator;

    public void NewSteak()
    {
        steakGenerator.GenerateMeat(true);
    }
}
