using UnityEngine;

public class TriggerInteraction : BaseInteraction
{

    [SerializeField] bool once = false;
    bool locked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (once && locked) return;
        if (!other.gameObject.CompareTag("Player")) return;
        Interact();

    }

}
