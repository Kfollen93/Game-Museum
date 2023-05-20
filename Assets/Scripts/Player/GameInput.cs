using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnZoomAction;
    public event EventHandler OnZoomCanceled;
    public event EventHandler OnSettingsAction;
    private PlayerInputActions _playerInputActions;
    private bool _isKeyboardAndMouse = false;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("There is more than one GameInput instance!");
        Instance = this;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    public void Start()
    {
        _playerInputActions.Disable();
    }

    // Connected to welcome button.
    public void EnablePlayerInput() => _playerInputActions.Enable();
    public void DisablePlayerInput(bool disable) => _playerInputActions.Disable();


    private void OnEnable()
    {
        InputSystem.onActionChange += InputActionChangeCallback;
        _playerInputActions.Player.Interact.performed += Interact_performed;
        _playerInputActions.Player.Zoom.performed += Zoom_performed;
        _playerInputActions.Player.Zoom.canceled += Zoom_canceled;
        _playerInputActions.Player.Settings.performed += Settings_performed;
    }

    private void Settings_performed(InputAction.CallbackContext obj)
    {
        OnSettingsAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= InputActionChangeCallback;
        _playerInputActions.Player.Interact.performed -= Interact_performed;
        _playerInputActions.Player.Zoom.performed -= Zoom_performed;
        _playerInputActions.Player.Zoom.canceled -= Zoom_canceled;
        _playerInputActions.Player.Settings.performed -= Settings_performed;
    }

    private void Zoom_performed(InputAction.CallbackContext obj)
    {
        OnZoomAction?.Invoke(this, EventArgs.Empty);
    }

    private void Zoom_canceled(InputAction.CallbackContext obj)
    {
        OnZoomCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    private void InputActionChangeCallback(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            InputAction receivedInputAction = (InputAction)obj;
            InputDevice lastDevice = receivedInputAction.activeControl.device;
            _isKeyboardAndMouse = lastDevice.name.Equals("Keyboard") || lastDevice.name.Equals("Mouse");
        }
    }

    public bool IsKeyboardAndMouseActive() => _isKeyboardAndMouse;
}
