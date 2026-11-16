using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButton :
    MonoBehaviour,
    IPointerDownHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private int _level;
    [SerializeField] private Popup _popup;
    [SerializeField] private MedalDisplay _medals;
    private const float BUBBLE_SPEED = 0.5f;
    private const float BUBBLE_SIZE = 1.15f;

    void Start()
    {
        List<RaceDetails> raceDetails = PersistentData.Instance.Levels[_level].Details;
        bool bronze = raceDetails[0].MedalEarned;
        bool silver = raceDetails[1].MedalEarned;
        bool gold = raceDetails[2].MedalEarned;
        _medals.SetMedals(bronze, silver, gold);
    }

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
