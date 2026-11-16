using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton :
    MonoBehaviour,
    IPointerDownHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private int _level;
    [SerializeField] private Popup _popup;
    [SerializeField] private MedalDisplay _medals;
    [SerializeField] private Image _lockIcon;
    [SerializeField] private Image _previewImage;
    private const float BUBBLE_SPEED = 0.5f;
    private const float BUBBLE_SIZE = 1.15f;
    private bool _locked = true;

    void Start()
    {
        LevelDetails levelDetails = PersistentData.Instance.Levels[_level];
        _locked = levelDetails.Locked;
        SetLocked(_locked);

        List<RaceDetails> raceDetails = levelDetails.Details;
        bool bronze = raceDetails[0].MedalEarned;
        bool silver = raceDetails[1].MedalEarned;
        bool gold = raceDetails[2].MedalEarned;
        _medals.SetMedals(bronze, silver, gold);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_locked) return;
        _popup.Show();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_locked) return;
        StopAllCoroutines();
        StartCoroutine(BubbleOut());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_locked) return;
        StopAllCoroutines();
        StartCoroutine(BubbleIn());
    }

    private void SetLocked(bool locked)
    {
        _lockIcon.gameObject.SetActive(locked);
        _previewImage.color = locked ? new Color(0.5f, 0.5f, 0.5f, 1.0f) : Color.white;
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
