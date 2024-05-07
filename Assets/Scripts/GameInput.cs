using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact;
        playerInputActions.Player.InteractAlternative.performed += InteractAlternative;
    }

    private void Interact(CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternative(CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        var input = playerInputActions.Player.Move.ReadValue<Vector2>();

        return input.normalized;
    }
}
