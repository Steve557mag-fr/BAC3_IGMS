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


    Vector2 moveValue;
    bool isLocked = true;

    public void OnInteract(InputValue val)
    {

        if(Physics.Raycast(head.transform.position, head.transform.forward, out RaycastHit hit, interactDistance))
        {
            BaseInteraction interaction = hit.transform.GetComponent<BaseInteraction>();
            if (interaction) interaction.Interact();
        }
        
    }

    public void OnMove(InputValue val)
    {
        moveValue = val.Get<Vector2>();
    }

    public void OnLook(InputValue val)
    {
        var lookValue = val.Get<Vector2>();
        head.transform.localEulerAngles -= Vector3.right * lookValue.y * headSensibility * Time.deltaTime; 
        transform.localEulerAngles += Vector3.up * lookValue.x * headSensibility * Time.deltaTime;

        head.transform.localEulerAngles = new(
            Mathf.Clamp(head.transform.localEulerAngles.x, -90f, 90f),
            head.transform.localEulerAngles.y,
            head.transform.localEulerAngles.z
        );

    }


    public void Update()
    {
        controller.Move(
            (transform.forward * moveValue.y + transform.right * moveValue.x).normalized * walkSpeed * Time.deltaTime
        );
        controller.Move(Physics.gravity);

        if (Keyboard.current[Key.O].wasPressedThisFrame) isLocked = !isLocked;
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;

        if (Physics.Raycast(head.transform.position, head.transform.forward, out RaycastHit hit, interactDistance))
            ui.UpdateInteract(hit.transform.GetComponent<BaseInteraction>() != null);
        else ui.UpdateInteract(false);


    }

}
