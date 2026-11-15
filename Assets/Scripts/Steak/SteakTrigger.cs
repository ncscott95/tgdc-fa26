using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SteakTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Steak steak = other.GetComponent<Steak>();
        if (steak != null)
        {
            OnSteakEnter(steak);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Steak steak = other.GetComponent<Steak>();
        if (steak != null)
        {
            OnSteakExit(steak);
        }
    }

    public virtual void OnSteakEnter(Steak steak)
    {
        
    }

    public virtual void OnSteakExit(Steak steak)
    {
        
    }
}
