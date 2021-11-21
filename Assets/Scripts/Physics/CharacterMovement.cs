using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marco Cornejo, November 18th 2021
public class CharacterMovement : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundCheck _groundCheck;

	[Header("Horizontal Movement Customization")]
	[SerializeField] private bool _canMove = true;
    [SerializeField] private float _turnSpeed = 5f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _acceleration = 10f;

    [Header("Vertical Movement Customization")]
    [SerializeField] private float _manualGravity = 20f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _airControlPercentage = 0.1f;

	[Header("Feedback")]
	[SerializeField] private Vector3 _moveInput;
    [SerializeField] private Vector3 _lookDirection;

    //Unity Messages ______________________________________________
    private void Awake()
    {
		_rigidbody = GetComponent<Rigidbody>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
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
    }

    //Custom Methods _______________________________________________
    private void ApplyMovementPhysics()
    {
        if (!_canMove) return;

        float control = _groundCheck.IsGrounded ? 1f : _airControlPercentage;

        Vector3 targetVelocity = _moveInput * _speed;
        Vector3 velocityDiff = targetVelocity - _rigidbody.velocity;
        velocityDiff.y = 0;

        Vector3 acceleration = velocityDiff * _acceleration * control;
        acceleration -= Vector3.up * _manualGravity; //Apply extra gravity
        _rigidbody.AddForce(acceleration);

    }

    private void ApplyLookDirection()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_lookDirection);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(rotation);
    }

    private void SetLookDirection(Vector3 direction)
    {
        direction.y = 0f;
        _lookDirection = direction.normalized;
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
        if (!_groundCheck.IsGrounded) return;
        float jumpSpeed = Mathf.Sqrt(2f * _manualGravity * _jumpHeight);
        Vector3 jumpVelocity = _rigidbody.velocity;
        jumpVelocity.y = jumpSpeed;
        _rigidbody.velocity = jumpVelocity;
    }

}
