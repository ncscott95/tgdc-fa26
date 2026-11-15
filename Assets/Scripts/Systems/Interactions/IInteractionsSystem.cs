using UnityEngine.EventSystems;

public interface IInteractionsSystem
{
    public void Click(Steak steak);
    public void Hover(Steak steak);
    public void Unhover(Steak steak);
    public void Drag(Steak steak, PointerEventData pointerEvent);
    public void EndDrag(Steak steak);
}
