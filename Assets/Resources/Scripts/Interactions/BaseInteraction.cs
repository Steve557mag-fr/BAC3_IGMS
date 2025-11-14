using UnityEngine;
using UnityEngine.Events;

public class BaseInteraction : MonoBehaviour
{
    public bool once = false;
    [SerializeField] protected UnityEvent onInteract;

    protected bool locked = false;

    public virtual void Interact()
    {
        if (locked && once) return;
        locked = true;
        onInteract.Invoke();
    }


}
