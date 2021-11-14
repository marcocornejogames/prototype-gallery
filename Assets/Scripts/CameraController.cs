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
	private void OnMove(InputValue input)
    {

		_movementInput = input.Get<Vector2>();
    }

	private void OnRotate(InputValue input)
    {
		_rotationInput = input.Get<float>();
    }

	private void OnScroll(InputValue input)
    {
		_scrollInput = input.Get<Vector2>();

		_scrollEvent.Invoke(_scrollInput.y);
    }
}
