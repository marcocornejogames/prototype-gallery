using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marco, November 13th 2021
public class SimpleMovement : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Rigidbody _rigidbody;
	[Header("Customization")]
	[SerializeField] private float _movementSpeed;
	[SerializeField] private float _movementAcceleration;

	[Header("Feedback")]
	[SerializeField] private Vector2 _movementInput;


    //Unity Messages ______________________________________________
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
 
    }

   	void Update()
	{
    }
    private void FixedUpdate()
    {
        ApplyMovement();
    }

    //Custom Methods _______________________________________________
    private void ApplyMovement()
    {
        Vector3 controlRight = Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward);
        Vector3 controlForward = Vector3.Cross(Camera.main.transform.right, Vector3.up);
        if(_rigidbody.velocity.magnitude <= _movementSpeed) _rigidbody.AddRelativeForce(((_movementInput.x * controlRight) + (_movementInput.y * controlForward)) * _movementAcceleration);
    }
    public void UpdateMovementInput(Vector2 input)
    {
		_movementInput = input;
    }
}
