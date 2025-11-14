using COL1.Utilities;
using UnityEngine;

public class TeleportInteraction : BaseInteraction
{
    [Space(5)]
    [SerializeField] string sceneName;

    public override void Interact()
    {
        Singleton.Get<Game>().Goto(sceneName);
    }

}
