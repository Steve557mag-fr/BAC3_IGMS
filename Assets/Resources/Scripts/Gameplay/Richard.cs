using COL1.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Richard : MonoBehaviour
{
    public NavMeshAgent agent;
    public DialogInteraction interaction;

    public void Call()
    {
        agent.enabled = true;
        agent.SetDestination(Singleton.Get<PlayerController>().transform.position);
        Singleton.Get<PlayerController>().DisableCharacter();
        Singleton.Get<PlayerController>().LookAt(transform);
    }

    private void Awake()
    {
        agent.enabled = false;
    }

    public void Update()
    {

        if (!agent.enabled) return;

        if(agent.remainingDistance < 0.1f)
        {
            interaction.Interact();
            agent.enabled = false;
        }

    }


}
