using UnityEngine;

public class DialogInteraction : BaseInteraction
{

    [Space(5)]
    [SerializeField] int dialogID;

    public override void Interact()
    {
        Dialog.Get().NewFragment(dialogID);
    }

}
