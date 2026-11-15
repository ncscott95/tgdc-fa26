using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButton :
    MonoBehaviour,
    IPointerDownHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private Popup _popup;
    private const float BUBBLE_SPEED = 0.5f;
    private const float BUBBLE_SIZE = 1.15f;

    public void OnPointerDown(PointerEventData eventData)
    {
        _popup.Show();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(BubbleOut());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(BubbleIn());
    }

    private IEnumerator BubbleOut()
    {
        yield return BasicAnimations.Interpolate(
            null,
            t =>
            {
                float smooth = BasicAnimations.EaseOutElastic(t);
                transform.localScale = Vector3.one * Mathf.Lerp(1.0f, BUBBLE_SIZE, smooth);
            },
            () => transform.localScale = Vector3.one * BUBBLE_SIZE,
            BUBBLE_SPEED
        );
    }

    private IEnumerator BubbleIn()
    {
        yield return BasicAnimations.Interpolate(
            null,
            t =>
            {
                float smooth = BasicAnimations.EaseOutElastic(t);
                transform.localScale = Vector3.one * Mathf.Lerp(BUBBLE_SIZE, 1.0f, smooth);
            },
            () => transform.localScale = Vector3.one,
            BUBBLE_SPEED
        );
    }
}
