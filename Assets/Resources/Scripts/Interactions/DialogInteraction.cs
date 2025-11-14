using COL1.Utilities;
using UnityEngine;

public class DialogInteraction : BaseInteraction
{

    [Space(5)]
    [SerializeField] int dialogID;

    public override void Interact()
    {
        onInteract?.Invoke();
        Singleton.Get<Dialog>().NewFragment(dialogID);
    }

}
