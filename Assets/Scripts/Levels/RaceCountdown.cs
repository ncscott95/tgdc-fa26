using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCountdown : MonoBehaviour
{
    private const float PopupSpeed = 0.15f;
    private const float StayUpDuration = 0f;
    [SerializeField] private Transform _popupWindow;
    [SerializeField] private Image _image;
    [SerializeField] private List<Sprite> _sprites;

    public IEnumerator DoCountdownCoroutine()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < _sprites.Count - 1; i++)
        {
            _image.sprite = _sprites[i];
            yield return ShowRoutine();
            yield return new WaitForSeconds(StayUpDuration);
            yield return HideRoutine();
        }

        // _image.sprite = _sprites[^1];
        // yield return ShowRoutine();
    }

    public IEnumerator HideGoCoroutine()
    {
        _image.sprite = _sprites[^1];
        yield return ShowRoutine();
        yield return new WaitForSeconds(StayUpDuration);
        yield return StartCoroutine(HideRoutine());
        gameObject.SetActive(false);
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
            PopupSpeed * 4f
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
            () => _popupWindow.localScale = Vector3.zero,
            PopupSpeed
        );
    }
}