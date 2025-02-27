using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputSystem : MonoBehaviour
{
    public PlayerInput playerInput;
    InputAction MoveAction;

    public AnimationCurve MovementCurve;

    private Vector2 CurrentMovementVector;
    private Vector2 TargetMovementVector;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        MoveAction = playerInput.actions.FindActionMap("Base").FindAction("Movement");
        
        MoveAction.performed += MoveAction_performed;
        
    }

    private void MoveAction_performed(InputAction.CallbackContext callbackContext)
    {
        CurrentMovementVector = callbackContext.ReadValue<Vector2>();
        
        // CurrentMovementVector = Vector2.Lerp(CurrentMovementVector, TargetMovementVector, Time.deltaTime);

        Debug.Log(CurrentMovementVector);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
