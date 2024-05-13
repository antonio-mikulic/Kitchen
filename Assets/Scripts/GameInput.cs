using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDING_OVERRIDES_KEY = "PlayerBindingOverrides";
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;


    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Interact_Alternative,
        Pause,
        Gamepad_Interact,
        Gamepad_Interact_Alternative,
        Gamepad_Pause
    }

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact;
        playerInputActions.Player.InteractAlternative.performed += InteractAlternative;
        playerInputActions.Player.Pause.performed += PausePerformed;

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDING_OVERRIDES_KEY))
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDING_OVERRIDES_KEY));
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

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.Interact_Alternative:
                return playerInputActions.Player.InteractAlternative.bindings[0].ToDisplayString();
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Gamepad_Interact:
                return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_Interact_Alternative:
                return playerInputActions.Player.InteractAlternative.bindings[1].ToDisplayString();
            case Binding.Gamepad_Pause:
                return playerInputActions.Player.Pause.bindings[1].ToDisplayString();
            default:
                return "";
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction action;
        int bindingIndex;

        switch (binding)
        {
            case Binding.Move_Up:
                action = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                action = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                action = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                action = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                action = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Interact_Alternative:
                action = playerInputActions.Player.InteractAlternative;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                action = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_Interact:
                action = playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Interact_Alternative:
                action = playerInputActions.Player.InteractAlternative;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Pause:
                action = playerInputActions.Player.Pause;
                bindingIndex = 1;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(binding), binding, null);
        }

        action.PerformInteractiveRebinding(bindingIndex).OnComplete(operation =>
        {
            playerInputActions.Player.Enable();
            operation.Dispose();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDING_OVERRIDES_KEY, playerInputActions.SaveBindingOverridesAsJson());

            onActionRebound();
        }).Start();
    }
}
