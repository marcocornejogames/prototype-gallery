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
    [SerializeField] private float _turnSpeed = 5f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] public bool CanMove { get; private set; } = true;

    [Header("Vertical Movement")]
    [SerializeField] private float _manualGravity = 20f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _airControlPercentage = 0.1f;
    [SerializeField] public bool CanJump { get; private set; } = true;

    [Header("Crouching")]
    [SerializeField] private float _crouchSpeed = 5f;
    [SerializeField] private float _crouchAcceleration = 5f;
    [SerializeField] public bool CanCrouch { get; private set; } = true;

    [Header("Sprinting")]
    [SerializeField] private bool _canSprint = true;
    [SerializeField] private float _sprintSpeed = 15f;
    [SerializeField] private float _sprintAcceleration = 10f;

    [Header("Stunts")]
    [SerializeField] private float _tipForce = 5f;

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

        if (!CanMove) stateAcceleration = 0;
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
        if (!CanMove) return;
        direction.y = 0f;
        _lookDirection = direction.normalized;
    }

    public void Trip()
    {
        if (!_groundCheck.IsGrounded) return;
        EnableMovement(false);
        ToggleSprint(false);


        _animationController.OnTrip();
        _rigidbody.AddForce(_moveInput * _tipForce);
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
        if (!_groundCheck.IsGrounded || !CanJump) return;

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
        else if (CanCrouch)
        {
            CurrentMovementMode = MovementMode.Crouching;
        }
    }

    public void ToggleSprint(bool isSprinting)
    {
        if (isSprinting && _canSprint) CurrentMovementMode = MovementMode.Sprinting;
        else CurrentMovementMode = MovementMode.Regular;
    }

    public void EnableMovement(bool canMove)
    {
        CanMove = canMove;
        CanJump = canMove;
        _canSprint = canMove;
        CanCrouch = canMove;
    }
}
