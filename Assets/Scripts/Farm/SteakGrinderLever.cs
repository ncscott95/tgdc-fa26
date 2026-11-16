using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SteakGrinderLever : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SteakGrinder _grinder;
    [SerializeField] private Sprite _leverUpSprite;
    [SerializeField] private Sprite _leverDownSprite;

    private SpriteRenderer _leverRenderer;
    private bool _canPull = true;

    void Awake()
    {
        _leverRenderer = GetComponent<SpriteRenderer>();
        _leverRenderer.sprite = _leverUpSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_canPull) return;

        _grinder.OnPullLever();
        StartCoroutine(ResetLeverCoroutine());
    }

    private IEnumerator ResetLeverCoroutine()
    {
        _canPull = false;
        _leverRenderer.sprite = _leverDownSprite;

        yield return new WaitForSeconds(0.5f);

        _leverRenderer.sprite = _leverUpSprite;
        _canPull = true;
    }
}
