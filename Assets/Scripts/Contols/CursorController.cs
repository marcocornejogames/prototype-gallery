using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Marco, November 14th
public class CursorController : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private GameEvent<Vector2> _cursorPositionEvent;

	[Header("Customization")]
	[SerializeField] private LayerMask _hoverableLayerMask;

	[Header("Feedback")]
	[SerializeField] private Vector3 _cursorPosition;
	[SerializeField] private iHoverable _currentlyHovering;
	[SerializeField] private float _mouseClickAxis;
	[SerializeField] private Ray _mouseRay;

	//Unity Messages ______________________________________________
	void Start()
    {

    }

   	void Update()
	{
		UpdateRay();
		CheckIsHovering();
    }

	//Custom Methods _______________________________________________

	private void UpdateRay()
    {
		_mouseRay = Camera.main.ScreenPointToRay(_cursorPosition);
	}

	private void CheckIsHovering()
    {

		if(Physics.Raycast(_mouseRay, out RaycastHit hitInfo, Mathf.Infinity, _hoverableLayerMask))
        {
			if(hitInfo.collider.gameObject.TryGetComponent<iHoverable>(out iHoverable hoverableObject))
            {
				if (_currentlyHovering == hoverableObject) return; // Hovering over the same object
				
				_currentlyHovering?.Hover(false); //Hovering over new object
				_currentlyHovering = hoverableObject;
				_currentlyHovering.Hover(true);
				return;
            }
        }

		_currentlyHovering?.Hover(false); //Not hovering over hoverable object
		_currentlyHovering = null;



    }

	//Input Messages _______________________________________________
	private void OnMouseClick(InputValue input)
	{
		_mouseClickAxis = input.Get<float>();

	}

	private void OnCursorMove(InputValue input)
    {
		_cursorPosition = input.Get<Vector2>();
    }
}
