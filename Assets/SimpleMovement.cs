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
        transform.LookAt(new Vector3(Camera.main.transform.forward.x, transform.position.y, Camera.main.transform.forward.z));
        if(_rigidbody.velocity.magnitude <= _movementSpeed) _rigidbody.AddForce(new Vector3(_movementInput.x, 0f, _movementInput.y) * _movementAcceleration);
    }
    public void UpdateMovementInput(Vector2 input)
    {
		_movementInput = input;
    }
}
