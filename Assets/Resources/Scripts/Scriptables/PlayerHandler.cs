using COL1.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHandler", menuName = "Scriptable Objects/PlayerHandler")]
public class PlayerHandler : ScriptableObject
{
    [SerializeField] PlayerMethod methods;
    [SerializeField] StepType stepType;

    public void Interact()
    {
        switch (methods)
        {
            case PlayerMethod.PLAYER_DISABLE:
                Singleton.Get<PlayerController>().DisableCharacter();
                break;
            case PlayerMethod.STEP_TYPE:
                Singleton.Get<PlayerController>().SetWalkType(stepType);
                break;
            case PlayerMethod.SET_SPAWNPOINT:
                Singleton.Get<PlayerController>().SetSpawnpoint();
                break;
        }
    }

}

enum PlayerMethod
{
    PLAYER_DISABLE, STEP_TYPE, SET_SPAWNPOINT
}

