using UnityEngine.EventSystems;

public class InteractionManager : Singleton<InteractionManager>, IInteractionsSystem
{
    private IInteractionsSystem _interactionsSystem;

    public void SetInteractionSystem(IInteractionsSystem system)
    {
        _interactionsSystem = system;
    }

    public void RemoveInteractionSystem(IInteractionsSystem system)
    {
        if (system == _interactionsSystem) _interactionsSystem = null;
    }

    public void Click(Steak steak)
    {
        _interactionsSystem?.Click(steak);
    }

    public void Hover(Steak steak)
    {
        _interactionsSystem?.Hover(steak);
    }

    public void Unhover(Steak steak)
    {
        _interactionsSystem?.Unhover(steak);
    }

    public void Drag(Steak steak, PointerEventData pointerEvent)
    {
        _interactionsSystem?.Drag(steak, pointerEvent);
    }  

    public void EndDrag(Steak steak)
    {
        _interactionsSystem?.EndDrag(steak);
    }  
}
