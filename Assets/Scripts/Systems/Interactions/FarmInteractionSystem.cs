using UnityEngine;
using UnityEngine.EventSystems;

public class FarmInteractionSystem : InteractionSystemBase
{
    private Steak _draggedSteak;
    Vector2 _dragTarget;

    public override void Click(Steak steak)
    {
        
    }

    public override void Drag(Steak steak, PointerEventData pointerEvent)
    {
        _draggedSteak = steak;
        _dragTarget = Camera.main.ScreenToWorldPoint(pointerEvent.position);
    }

    public override void EndDrag(Steak steak)
    {
        _draggedSteak = null;
    }

    public override void Hover(Steak steak)
    {
        
    }

    public override void Unhover(Steak steak)
    {
        
    }

    public void FixedUpdate()
    {
        _draggedSteak?.Drag(_dragTarget);
    }
}
