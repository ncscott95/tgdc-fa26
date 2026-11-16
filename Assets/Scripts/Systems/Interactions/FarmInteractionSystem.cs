using UnityEngine;
using UnityEngine.EventSystems;

public class FarmInteractionSystem : InteractionSystemBase
{
    [SerializeField] private SteakDataDisplay _dataDisplay;

    private Steak _draggedSteak;
    Vector2 _dragTarget;
    private bool _isDisabled = false;

    public override void Click(Steak steak)
    {
        
    }

    public override void Drag(Steak steak, PointerEventData pointerEvent)
    {
        if (_isDisabled) return;

        _draggedSteak = steak;
        _dragTarget = Camera.main.ScreenToWorldPoint(pointerEvent.position);
        _dataDisplay.ShowDisplay(steak.Data);
    }

    public override void EndDrag(Steak steak)
    {
        _draggedSteak = null;
        _dataDisplay.HideDisplay();
    }

    public override void Hover(Steak steak)
    {
        
    }

    public override void Unhover(Steak steak)
    {
        
    }

    public void FixedUpdate()
    {
        if (_isDisabled)
        {
            if (_draggedSteak != null)
            {
                EndDrag(_draggedSteak);
            }
            return;
        }

        _draggedSteak?.Drag(_dragTarget);
    }

    public void SetDisabled(bool isDisabled)
    {
        _isDisabled = isDisabled;
    }
}
