using UnityEngine;
using UnityEngine.UI;

public class ButtonSquelch : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference _buttonSquelch;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    public void Click()
    {
        AudioManager.Instance.Play(_buttonSquelch);
    }
}
