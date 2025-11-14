using System;
using COL1.Utilities;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] UIPlayer ui;
    [SerializeField] Camera head;
    [SerializeField] CharacterController controller;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float walkSpeed;
    [SerializeField] float headSensibility;
    [SerializeField] float interactDistance = 10;

    Vector2 headRotation;
    Vector2 moveValue;
    bool isCursorLocked = true;

    Action onMoveFinished;

    public void OnInteract()
    {
        if (!Physics.Raycast(head.transform.position, head.transform.forward, out RaycastHit hit, interactDistance)) return;
        
        BaseInteraction interaction = hit.transform.GetComponent<BaseInteraction>();
        if (interaction != null) interaction.Interact();
    }

    public void OnMove(InputValue val)
    {
        moveValue = val.Get<Vector2>();
    }

    public void OnLook(InputValue val)
    {
        var lookValue = val.Get<Vector2>();

        headRotation.x -= lookValue.y * headSensibility * Time.deltaTime;
        headRotation.y += lookValue.x * headSensibility * Time.deltaTime;

        headRotation.x = Mathf.Clamp(headRotation.x, -80, 80);
        head.transform.localRotation = Quaternion.Euler(Vector3.right * headRotation.x);
        transform.localRotation = Quaternion.Euler(Vector3.up * headRotation.y);

    }
    
    public void OnLeftClick(InputValue val)
    {
        Singleton.Get<Dialog>().Next();
    }

    private void Awake()
    {
        Singleton.Make(this);    
    }

    public void Update()
    {
        
        if(controller.enabled)
        {
            controller.Move(
                (transform.forward * moveValue.y + transform.right * moveValue.x).normalized * walkSpeed * Time.deltaTime
            );
            controller.Move(Physics.gravity * Time.deltaTime);
        }
         
        if (Keyboard.current[Key.Tab].wasPressedThisFrame) isCursorLocked = !isCursorLocked;
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        
        if (Physics.Raycast(head.transform.position, head.transform.forward, out RaycastHit hit, interactDistance))
            ui.UpdateInteract(hit.transform.GetComponent<BaseInteraction>() != null);
        else ui.UpdateInteract(false);

        if (agent.enabled && agent.remainingDistance < 0.1f)
        {
            onMoveFinished?.Invoke();
            onMoveFinished = null;
            EnableCharacter();
        }

    }

    public void MoveTo(Transform destination)
    {
        agent.enabled = true;
        controller.enabled = false;
        agent.SetDestination(destination.position);
    }

    public void EnableCharacter()
    {
        agent.enabled = false;
        controller.enabled = true;
    }

    public void DisableCharacter()
    {
        controller.enabled = false;
    }

    public void LookAt(Transform target)
    {
        //TODO
        transform.LookAt(target.position, Vector3.up);
        head.transform.LookAt(target.position);
    }

}
