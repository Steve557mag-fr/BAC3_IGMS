using COL1.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHandler", menuName = "Scriptable Objects/PlayerHandler")]
public class PlayerHandler : ScriptableObject
{
    [SerializeField] PlayerMethod methods;
    
    public void Interact()
    {
        switch (methods)
        {
            case PlayerMethod.PLAYER_DISABLE:
                Singleton.Get<PlayerController>().DisableCharacter();
                break;
        }
    }

}

enum PlayerMethod
{
    PLAYER_DISABLE
}

