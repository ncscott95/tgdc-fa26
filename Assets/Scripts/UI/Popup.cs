using System.Collections;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private Transform _popupWindow;
    private const float POPUP_SPEED = 0.25f;

    public void Show()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        StartCoroutine(ShowRoutine());
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        yield return BasicAnimations.Interpolate(
            null,
            t =>
            {
                float smooth = BasicAnimations.EaseOutElasticWeak(t);
                _popupWindow.localScale = Vector3.one * smooth;
            },
            () => _popupWindow.localScale = Vector3.one,
            POPUP_SPEED * 4f
        );
    }

    private IEnumerator HideRoutine()
    {
        yield return BasicAnimations.Interpolate(
            null,
            t =>
            {
                float smooth = BasicAnimations.EaseOut(t);
                _popupWindow.localScale = Vector3.one * (1.0f - smooth);
            },
            () => gameObject.SetActive(false),
            POPUP_SPEED
        );
    }
}
