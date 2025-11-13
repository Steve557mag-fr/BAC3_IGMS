using UnityEngine;

public class TeleportInteraction : BaseInteraction
{
    [Space(5)]
    [SerializeField] string sceneName;

    public override void Interact()
    {
        Game.Get().Goto(sceneName);
    }

}
