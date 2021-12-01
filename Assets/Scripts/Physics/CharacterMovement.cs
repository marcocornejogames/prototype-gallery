using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marco Cornejo, November 18th 2021
public class CharacterMovement : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private ThirdPersonAnimation _animationController;

    [Header("Horizontal Movement")]
    [SerializeField] private bool _canMove = true;
    [SerializeField] private float _turnSpeed = 5f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _acceleration = 10f;

    [Header("Vertical Movement")]
    [SerializeField] private float _manualGravity = 20f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _airControlPercentage = 0.1f;
    [SerializeField] private bool _canJump = true;

    [Header("Crouching")]
    [SerializeField] private bool _canCrouch = true;
    [SerializeField] private float _crouchSpeed = 5f;
    [SerializeField] private float _crouchAcceleration = 5f;

    [Header("Sprinting")]
    [SerializeField] private bool _canSprint = true;
    [SerializeField] private float _sprintSpeed = 15f;
    [SerializeField] private float _sprintAcceleration = 10f;

    [Header("Feedback")]
    [SerializeField] private Vector3 _moveInput;
    [SerializeField] private Vector3 _lookDirection;
    [SerializeField] public MovementMode CurrentMovementMode { get; private set; } = MovementMode.Regular;

    public Vector3 GroundNormal => _groundCheck.GroundNormal;
    public enum MovementMode
    {
        Regular,
        Crouching,
        Sprinting
    }

    //Unity Messages ______________________________________________
    private void Awake()
    {
		_rigidbody = GetComponent<Rigidbody>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _animationController = GetComponentInChildren<ThirdPersonAnimation>();
    }
    void Start()
    {

    }

   	void Update()
	{

    }
    private void FixedUpdate()
    {
        ApplyMovementPhysics();
        ApplyLookDirection();
        UpdateMovementAnimation();
    }

    //Custom Methods _______________________________________________
    private void ApplyMovementPhysics()
    {
        if (!_canMove) return;

        float stateAcceleration = 0;
        float stateSpeed = 0;

        switch (CurrentMovementMode)
        {
            case MovementMode.Regular:
                stateAcceleration = _acceleration;
                stateSpeed = _speed;
                break;

            case MovementMode.Crouching:
                stateAcceleration = _crouchAcceleration;
                stateSpeed = _crouchSpeed;
                break;

            case MovementMode.Sprinting:
                stateAcceleration = _sprintAcceleration;
                stateSpeed = _sprintSpeed;
                break;
        }


        float control = _groundCheck.IsGrounded ? 1f : _airControlPercentage;

        Vector3 targetVelocity = _moveInput * stateSpeed;
        Vector3 velocityDiff = targetVelocity - _rigidbody.velocity;
        velocityDiff.y = 0;

        Vector3 acceleration = velocityDiff * stateAcceleration * control;
        acceleration -= GroundNormal * _manualGravity; //Apply extra gravity
        _rigidbody.AddForce(acceleration);

    }

    private void ApplyLookDirection()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_lookDirection);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(rotation);

        _animationController.UpdateRotationAnimation(rotation, targetRotation);
        _animationController.SetIsGrounded(_groundCheck.IsGrounded);
    }

    private void SetLookDirection(Vector3 direction)
    {
        direction.y = 0f;
        _lookDirection = direction.normalized;
    }

    public void Trip()
    {
        LockAllMovement();
    }

    private void LockAllMovement()
    {
        _canMove = false;
        _canJump = false;
        _canSprint = false;
        _canCrouch = false;
    }
    //Animation _____________________________________________________
    private void UpdateMovementAnimation()
    {
        _animationController.SetMovementAxis(_moveInput);
        _animationController.ToggleCrouch(CurrentMovementMode == MovementMode.Crouching);
        _animationController.ToggleSprint(CurrentMovementMode == MovementMode.Sprinting);
    }

    //Event Calls __________________________________________________
    public void UpdateMoveInput(Vector2 moveInput)
    {
        Vector3 up = Vector3.up;
        Vector3 right = Camera.main.transform.right;
        Vector3 forward = Vector3.Cross(right, up); //Flat "camera" forward

        _moveInput = right * moveInput.x + forward * moveInput.y;

        SetLookDirection(forward);

    }

    public void TryJump()
    {
        if (!_groundCheck.IsGrounded || !_canJump) return;

        CurrentMovementMode = MovementMode.Regular;

        float jumpSpeed = Mathf.Sqrt(2f * _manualGravity * _jumpHeight);
        Vector3 jumpVelocity = _rigidbody.velocity;
        jumpVelocity.y = jumpSpeed;
        _rigidbody.velocity = jumpVelocity;
        _animationController.OnJump();
    }

    public void ToggleCrouch()
    {
        if(CurrentMovementMode == MovementMode.Crouching)
        {
            CurrentMovementMode = MovementMode.Regular;
        }
        else if (_canCrouch)
        {
            CurrentMovementMode = MovementMode.Crouching;
        }
    }

    public void ToggleSprint(bool isSprinting)
    {
        if (isSprinting && _canSprint) CurrentMovementMode = MovementMode.Sprinting;
        else CurrentMovementMode = MovementMode.Regular;
    }
}
