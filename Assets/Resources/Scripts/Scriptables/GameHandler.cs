using COL1.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "GameHandler", menuName = "Scriptable Objects/GameHandler")]
public class GameHandler : ScriptableObject
{

    [SerializeField] GameMethod methodType;
    public int paramInt;

    public void Interact()
    {
        switch (methodType)
        {
            case GameMethod.STEP_SET:
                Singleton.Get<Game>().SetStep(paramInt);
                break;
        }
    }

}

enum GameMethod
{
    STEP_SET,

}

