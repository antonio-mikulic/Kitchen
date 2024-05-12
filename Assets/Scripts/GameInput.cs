using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact;
        playerInputActions.Player.InteractAlternative.performed += InteractAlternative;
        playerInputActions.Player.Pause.performed += PausePerformed;
    }

    private void PausePerformed(CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact;
        playerInputActions.Player.InteractAlternative.performed -= InteractAlternative;
        playerInputActions.Player.Pause.performed -= PausePerformed;

        playerInputActions.Dispose();
    }
}
