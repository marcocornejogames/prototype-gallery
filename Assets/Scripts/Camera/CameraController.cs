using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Name, Date
public class CameraController : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private GameEvent<Vector2> _movementEvent;
	[SerializeField] private GameEvent<float> _rotationEvent;
	[SerializeField] private GameEvent<float> _scrollEvent;

	[Header("Feedback")]
	[SerializeField] private float _rotationInput;
	[SerializeField] private Vector2 _movementInput;
	[SerializeField] private Vector2 _scrollInput;


	//Unity Messages ______________________________________________
	void Start()
    {
    }

   	void Update()
	{
		BroadcastInput();
    }

	//Custom Methods _______________________________________________
	private void BroadcastInput()
    {
		_movementEvent.Invoke(_movementInput);
		_rotationEvent.Invoke(_rotationInput);
    }
	private void OnMoveCam(InputValue input)
    {

		_movementInput = input.Get<Vector2>();
    }

	private void OnRotateCam(InputValue input)
    {
		_rotationInput = input.Get<float>();
    }

	private void OnScrollCam
		(InputValue input)
    {
		_scrollInput = input.Get<Vector2>();

		float broadcastValue = 0;
		if(_scrollInput.y != 0)
        {
			if (_scrollInput.y > 0) broadcastValue = 1;
			else broadcastValue = -1;
        }
		_scrollEvent.Invoke(broadcastValue);
    }
}
