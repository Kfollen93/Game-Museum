using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    [SerializeField] private float _playerSpeed = 3.0f;
    private const float GRAVITY_VALUE = -9.81f;
    private Transform _cameraTransform;
    [SerializeField] private LayerMask _exhibitsLayerMask;
    private const float INTERACT_DISTANCE = 2f;
    private Exhibit _selectedExhibit;
    public event EventHandler<OnSelectedExhibitChangedEventArgs> OnSelectedExhibitChanged;
    public class OnSelectedExhibitChangedEventArgs : EventArgs
    {
        public Exhibit _selectedExhibitEventArg;
    }

    private void Awake()
    {
        if (Instance != null) Debug.LogError("There is more than one Player instance!");
        Instance = this;
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit, INTERACT_DISTANCE))
        {
            Exhibit exhibit = rayCastHit.transform.GetComponentInParent<Exhibit>();

            if (exhibit != null)
            {
                _selectedExhibit = exhibit;
                SetSelectedExhibit(_selectedExhibit);
            }
            else
            {
                _selectedExhibit = null;
                SetSelectedExhibit(null);
            }
        }
        else
        {
            _selectedExhibit = null;
            SetSelectedExhibit(null);
        }
    }

    private void SetSelectedExhibit(Exhibit selectedExhibit)
    {
        _selectedExhibit = selectedExhibit;
        OnSelectedExhibitChanged?.Invoke(this, new OnSelectedExhibitChangedEventArgs
        {
            _selectedExhibitEventArg = _selectedExhibit
        });
    }

    private void HandleMovement()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector2 movement = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;
        move.y = 0;
        _controller.Move(_playerSpeed * Time.deltaTime * move);

        float deltaTimeMultiplier = GameInput.Instance.IsKeyboardAndMouseActive() ? 1.0f : Time.deltaTime;
        _playerVelocity.y += GRAVITY_VALUE * deltaTimeMultiplier;
        _controller.Move(_playerVelocity * deltaTimeMultiplier);
    }
}
