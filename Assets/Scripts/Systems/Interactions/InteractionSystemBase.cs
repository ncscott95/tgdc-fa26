using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractionSystemBase : MonoBehaviour, IInteractionsSystem
{
    public abstract void Click(Steak steak);
    public abstract void Drag(Steak steak, PointerEventData pointerEvent);
    public abstract void EndDrag(Steak steak);
    public abstract void Hover(Steak steak);
    public abstract void Unhover(Steak steak);

    public void Start()
    {
        InteractionManager.Instance.SetInteractionSystem(this);
    }

    public void OnDestroy()
    {
        InteractionManager.Instance.RemoveInteractionSystem(this);        
    }
}
