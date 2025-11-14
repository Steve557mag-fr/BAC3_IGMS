using UnityEngine;

public class TriggerInteraction : BaseInteraction
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Interact();

    }

}
