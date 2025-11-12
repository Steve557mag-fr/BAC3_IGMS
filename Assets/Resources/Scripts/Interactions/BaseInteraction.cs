using UnityEngine;
using UnityEngine.Events;

public class BaseInteraction : MonoBehaviour
{

    [SerializeField] protected UnityEvent onInteract;

    public virtual void Interact()
    {
        onInteract.Invoke();
    }


}
