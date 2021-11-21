using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Marco Cornejo, November 18th 2021
public class ThirdPersonPlayerController : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private GameEvent<Vector2> _onMoveEvent;
	[SerializeField] private GameEvent<Vector2> _onLookEvent;
	[SerializeField] private BoolEvent _onJumpEvent;

	[Header("Feedback")]
	[SerializeField] private Vector2 _moveInput;
	[SerializeField] private Vector2 _lookInput;

	//Unity Messages ______________________________________________
	void Start()
    {
    }

   	void Update()
	{
		UpdateInputBroadcast();
    }

	//Input Calls _______________________________________________
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
		_onJumpEvent.Invoke(true);
    }

	//Input Broadcasting _________________________________________
	private void UpdateInputBroadcast()
    {
		_onMoveEvent.Invoke(_moveInput);
		_onLookEvent.Invoke(_lookInput);
    }
}
