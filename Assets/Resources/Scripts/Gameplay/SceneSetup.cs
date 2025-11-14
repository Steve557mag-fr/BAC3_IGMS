using System.Collections.Generic;
using COL1.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] List<SceneInstruction> instructions;
    [SerializeField] GameObject[] stepContainers;

    public void Awake()
    {
        int step = Singleton.Get<Game>().gameData.step;
        for (int i = 0; i < instructions.Count; i++) {
            if (instructions[i].stepActivation != step) continue;
            instructions[i].instruction?.Invoke();
            return;
        }

        UpdateStep();
    }

    private void UpdateStep()
    {
        for (int i = 0; i < stepContainers.Length; i++) {
            stepContainers[
                Singleton.Get<Game>().gameData.step
                ].SetActive(false);
        }
    }

}


[System.Serializable]
public struct SceneInstruction
{
    public int stepActivation;
    public UnityEvent instruction;
}

