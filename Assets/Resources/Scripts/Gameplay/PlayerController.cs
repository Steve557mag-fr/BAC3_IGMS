using COL1.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] UIPlayer ui;
    [SerializeField] Camera head;
    [SerializeField] CharacterController controller;
    [SerializeField] float walkSpeed;
    [SerializeField] float headSensibility;
    [SerializeField] float interactDistance = 10;

    Vector2 headRotation;
    Vector2 moveValue;
    bool isLocked = true;

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
        print("dzqdqzddq");

        Dialog.Get().Next();

    }


    public void Update()
    {
        controller.Move(
            (transform.forward * moveValue.y + transform.right * moveValue.x).normalized * walkSpeed * Time.deltaTime
        );
        controller.Move(Physics.gravity);
         
        if (Keyboard.current[Key.Tab].wasPressedThisFrame) isLocked = !isLocked;
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;

        if (Physics.Raycast(head.transform.position, head.transform.forward, out RaycastHit hit, interactDistance))
            ui.UpdateInteract(hit.transform.GetComponent<BaseInteraction>() != null);
        else ui.UpdateInteract(false);


    }

}
