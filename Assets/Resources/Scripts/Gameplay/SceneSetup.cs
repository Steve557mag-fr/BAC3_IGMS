using System.Collections.Generic;
using COL1.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] UnityEvent onBegin;

    [SerializeField] List<SceneInstruction> instructions;
    [SerializeField] StepContainer[] stepContainers;

    public void Awake()
    {
        int step = Singleton.Get<Game>().gameData.step;
        for (int i = 0; i < instructions.Count; i++) {
            if (instructions[i].stepActivation != step) continue;
            instructions[i].instruction?.Invoke();
            return;
        }

        UpdateStep();
        onBegin?.Invoke();
    }

    public void UpdateStep()
    {
        for (int i = 0; i < stepContainers.Length; i++) {
            stepContainers[i].container.SetActive(Singleton.Get<Game>().gameData.step == stepContainers[i].stepActivation);
        }
    }

}


[System.Serializable]
public struct SceneInstruction
{
    public int stepActivation;
    public UnityEvent instruction;
}

[System.Serializable]
public struct StepContainer
{
    public int stepActivation;
    public GameObject container;
}