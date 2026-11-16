using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SteakCombinerButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SteakCombiner _combiner;

    private SpriteRenderer _buttonRenderer;
    private bool _canPush = true;
    private readonly Color _buttonUpColor = new Color(1f, 1f, 1f, 1f);
    private readonly Color _buttonDownColor = new Color(0.7f, 0.7f, 0.7f, 1f);

    void Awake()
    {
        _buttonRenderer = GetComponent<SpriteRenderer>();
        _buttonRenderer.color = _buttonUpColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_canPush) return;

        _combiner.OnPushButton();
        StartCoroutine(ResetLeverCoroutine());
    }

    private IEnumerator ResetLeverCoroutine()
    {
        _canPush = false;
        _buttonRenderer.color = _buttonDownColor;

        yield return new WaitForSeconds(0.5f);

        _buttonRenderer.color = _buttonUpColor;
        _canPush = true;
    }
}
