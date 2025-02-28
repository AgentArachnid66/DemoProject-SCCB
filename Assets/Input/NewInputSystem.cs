using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
// using static UnityEngine.InputSystem.InputAction;

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
        MoveAction.canceled += MoveAction_canceled;
        
    }

    private void MoveAction_canceled(InputAction.CallbackContext callbackContext)
    {
        CurrentMovementVector = Vector2.zero;
        TargetMovementVector = Vector2.zero;
    }

    private void MoveAction_performed(InputAction.CallbackContext callbackContext)
    {
        TargetMovementVector = callbackContext.ReadValue<Vector2>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveAction.IsPressed())
        {
            float multiplier = 
                MovementCurve.Evaluate(
                Mathf.Clamp(Mathf.InverseLerp(0, TargetMovementVector.magnitude, CurrentMovementVector.magnitude), 0, 1));
            
            CurrentMovementVector = Vector2.Lerp(CurrentMovementVector, TargetMovementVector, Time.deltaTime * Mathf.Max(multiplier, 0.01f));
            Debug.Log(CurrentMovementVector);
            Vector3 currentMove3D = CurrentMovementVector;

            gameObject.transform.SetPositionAndRotation(gameObject.transform.position + currentMove3D, gameObject.transform.rotation);
        }
    }
}
