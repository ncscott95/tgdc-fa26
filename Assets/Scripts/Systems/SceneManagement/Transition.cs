using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] private Image transition;
    private const float DURATION = 0.5f;

    void Start()
    {
        transition.gameObject.SetActive(false);
    }

    public IEnumerator FadeIn()
    {
        yield return BasicAnimations.Interpolate(
            () =>
            {
                transition.color = Color.clear;
                transition.gameObject.SetActive(true);    
            },
            t =>
            {
                float smooth = BasicAnimations.Smooth(t);
                transition.color = new Color(0.0f, 0.0f, 0.0f, smooth);
            },
            () => transition.color = Color.black,
            DURATION
        );
    }

    public IEnumerator FadeOut()
    {
        yield return BasicAnimations.Interpolate(
            null,
            t =>
            {
                float smooth = BasicAnimations.Smooth(t);
                transition.color = new Color(0.0f, 0.0f, 0.0f, 1.0f - smooth);
            },
            () => transition.gameObject.SetActive(false),
            DURATION
        );
    }
}
