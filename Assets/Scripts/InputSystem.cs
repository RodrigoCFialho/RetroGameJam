using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    private CustomInput customInput;

    private Vector2 moveInput;

    [SerializeField]
    private UnityEvent<Vector2> onEnableMovementEvent;

    [SerializeField]
    private UnityEvent onEnableDashEvent;

    [SerializeField]
    private UnityEvent onEnableShootEvent;

    private void Start()
    {
        customInput = CustomInputManager.Instance.customInputsBindings;

        customInput.Player.Movement.performed += InputMovementPerformed;
        customInput.Player.Movement.canceled += InputMovementCancelled;

        customInput.Player.Dash.performed += InputDashPerformed;

        customInput.Player.Shoot.performed += InputShootPerformed;
    }

    private void OnDisable()
    {
        customInput.Player.Movement.performed -= InputMovementPerformed;
        customInput.Player.Movement.canceled -= InputMovementCancelled;
    }

    private void InputMovementPerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        onEnableMovementEvent.Invoke(moveInput);
    }
    private void InputMovementCancelled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        onEnableMovementEvent.Invoke(moveInput);
    }

    private void InputDashPerformed(InputAction.CallbackContext context) {
        onEnableDashEvent.Invoke();
    }

    private void InputShootPerformed(InputAction.CallbackContext context) {
        onEnableShootEvent.Invoke();
    }
}
