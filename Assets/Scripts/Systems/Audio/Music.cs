using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference _music;

    void Start()
    {
        AudioManager.Instance.PlayMusic(_music);
    }
}
