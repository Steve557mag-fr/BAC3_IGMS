using System;
using COL1.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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

    [SerializeField] AudioSource stepHouse, stepStreet, stepShop;

    [SerializeField] float stepTimerRate = 1;
    float stepTimer = 0;

    StepType stepType;


    Vector3 oldPosition;
    Vector2 headRotation;
    Vector2 moveValue;
    bool isCursorLocked = true;
    bool isHeadLocked = false;
    bool isPaused = false;

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

        if (isHeadLocked) return; 

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

    public void OnPause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0.0f : 1.0f;
        ui.SetPause(isPaused);
    }


    private void Awake()
    {
        DisableCharacter();
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
        if (Keyboard.current[Key.M].wasPressedThisFrame) EnableCharacter();
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

        Vector3 vel = transform.position - oldPosition;
        oldPosition = transform.position;

        if(vel.magnitude >= 0.01f)
        {
            stepTimer -= Time.deltaTime;
            print(stepTimer);

            if (stepTimer <= 0)
            {
                stepTimer = stepTimerRate;
                switch (stepType)
                {
                    case StepType.HOUSE:
                        stepHouse.Play();
                        break;

                    case StepType.STREET:
                        stepStreet.Play();
                        break;

                    case StepType.SHOP:
                        stepShop.Play();
                        break;

                }
            }
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
        isHeadLocked = false;
    }

    public void DisableCharacter()
    {
        isHeadLocked = true;
        controller.enabled = false;
    }

    public void LookAt(Transform target)
    {
        //TODO
        transform.LookAt(target.position, Vector3.up);
        head.transform.LookAt(target.position);
    }

    public void SetWalkType(StepType newStepType)
    {
        stepType = newStepType;
    }

    public void SetSpawnpoint()
    {
        DisableCharacter();
        Vector3 p = FindAnyObjectByType<Spawnpoint>().transform.position;

        transform.position = p;
            
        EnableCharacter();

    }

}

public enum StepType
{
    HOUSE,
    STREET,
    SHOP
}
