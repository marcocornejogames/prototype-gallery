using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marco, November 25th
public class ThirdPersonAnimation : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private Animator _animator;
	[SerializeField] private BoolEvent _lockMovementEvent;

	[Header("Customization")]
	[SerializeField] private float _movementDampTime = 0.5f;


    //Unity Messages ______________________________________________
    private void Awake()
    {
		_animator = GetComponent<Animator>();
    }
    void Start()
    {

    }

   	void Update()
	{

    }

	//Custom Methods _______________________________________________
	public void SetMovementAxis(Vector3 movementAxis)
	{
		Vector3 localDirection = transform.InverseTransformDirection(movementAxis);

		_animator.SetFloat("Strafe", localDirection.x, _movementDampTime, Time.fixedDeltaTime);
		_animator.SetFloat("Forward", localDirection.z, _movementDampTime, Time.fixedDeltaTime);
	}

	public void UpdateRotationAnimation(Quaternion currentRotation, Quaternion targetRotation)
    {

    }

	//Set Int _________________________________________________________________________________
	public void SetActionType(int actionType)
    {
		_animator.SetInteger("ActionType", actionType);
    }
	public void SetAttackType(int attackType)
    {
		_animator.SetInteger("AttackType", attackType);
    }
	//Set Bools _______________________________________________________________________________
	public void SetIsGrounded(bool isGrounded)
    {
		_animator.SetBool("IsGrounded", isGrounded);
    }

	public void ToggleCrouch(bool isCrouching)
    {
		_animator.SetBool("IsCrouching", isCrouching);
    }

	public void ToggleSprint(bool isSprinting)
    {
		_animator.SetBool("IsSprinting", isSprinting);
    }
	
	//Animation Triggers ____________________________________________________________________________
	public void OnJump()
	{
		_animator.SetTrigger("Jump");
	}
	public void OnTrip()
	{
		_animator.SetTrigger("Trip");
	}
	public void OnAction()
    {
		_animator.SetTrigger("Action");
    }
	//Animation Events _______________________________________________________________________________

	private void OnStand()
    {
		_animator.ResetTrigger("Trip");
		_lockMovementEvent.Invoke(true);
    }
}
