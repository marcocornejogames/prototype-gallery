using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Name, Date
public class GroundCheck : MonoBehaviour
{

	[Header("Customization")]
	[SerializeField] private LayerMask _groundMask;
	[SerializeField] private float _groundCheckRadius = 0.25f;
	[SerializeField] private Vector3 _groundCheckStart = new Vector3(0f, 0.35f, 0f);
	[SerializeField] private Vector3 _groundCheckEnd = new Vector3(0, 0.1f, 0f);

	[SerializeField] public bool IsGrounded { get; private set; }
	[SerializeField] public Vector3 GroundNormal { get; private set; }

	//Unity Messages ______________________________________________
	void Start()
    {

    }

   	void Update()
	{



    }

    private void FixedUpdate()
    {
		IsGrounded = CheckIsGrounded();
	}

    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
		Vector3 start = transform.TransformPoint(_groundCheckStart);
		Vector3 end = transform.TransformPoint(_groundCheckEnd);

		Gizmos.DrawSphere(start, _groundCheckRadius);
		Gizmos.DrawSphere(end, _groundCheckRadius);
    }

    //Custom Methods _______________________________________________
    private bool CheckIsGrounded()
	{
		Vector3 start = transform.TransformPoint(_groundCheckStart);
		Vector3 end = transform.TransformPoint(_groundCheckEnd);
		float distance = Vector3.Distance(start, end);

		if(Physics.SphereCast(start, _groundCheckRadius, -transform.up, out RaycastHit hitInfo, distance, _groundMask))
        {
			GroundNormal = hitInfo.normal;
			return true;
        }

		GroundNormal = Vector3.up;

		return false;
	}
}
