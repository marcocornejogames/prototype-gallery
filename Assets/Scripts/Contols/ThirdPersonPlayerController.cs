using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// Marco Cornejo, November 18th 2021
public class ThirdPersonPlayerController : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private CharacterMovement _characterMovement;
	[SerializeField] private ThirdPersonAction _characterAction;
	[SerializeField] private UnityEvent<Vector2> _onLookEvent;

	[Header("Feedback")]
	[SerializeField] private Vector2 _moveInput;
	[SerializeField] private Vector2 _lookInput;

    //Unity Messages ______________________________________________
    private void Awake()
    {
		_characterMovement = GetComponentInParent<CharacterMovement>();
		_characterAction = GetComponentInParent<ThirdPersonAction>();
    }
    void Start()
    {

    }

   	void Update()
	{
		UpdateInputBroadcast();
    }

	//Input Sysem Calls _______________________________________________
	private void OnMove(InputValue input)
	{
		_moveInput = input.Get<Vector2>();
	}

	private void OnLook(InputValue input)
    {
		_lookInput = input.Get<Vector2>();
    }

	private void OnJump()
    {
		_characterMovement.TryJump();
    }

	private void OnCrouch()
    {
		_characterMovement.ToggleCrouch();
    }

	private void OnSprintStart()
    {
		_characterMovement.ToggleSprint(true);
    }

	private void OnSprintEnd()
    {
		_characterMovement.ToggleSprint(false);
    }

	private void OnActionLeft()
    {

    }

	private void OnActionRight()
    {

    }

	//Input Broadcasting _________________________________________
	private void UpdateInputBroadcast()
    {
		_characterMovement.UpdateMoveInput(_moveInput);
		_onLookEvent.Invoke(_lookInput);
    }
}
