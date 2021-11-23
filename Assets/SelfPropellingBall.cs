using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPropellingBall : MonoBehaviour
{
    [SerializeField] private TrajectoryPrediction _trajectoryPrediction;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _propellingForce = 10f;
    [SerializeField] private Rigidbody _rigidbody;
    private Ray _mouseRay;

    public Vector3 ProjectileForce;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trajectoryPrediction = GetComponent<TrajectoryPrediction>();
    }
    public void UpdateCursorRay(Ray ray)
    {
        _mouseRay = ray;
    }

    private void FixedUpdate()
    {
        CalculateForce();
        _trajectoryPrediction.Predict(ProjectileForce);
    }
    public void OnClick(bool onClick)
    {
        if (!onClick) return;
        ApplyForce();
    }
    private void ApplyForce()
    {

        _rigidbody.AddForce(ProjectileForce);
    }
    private Vector3 GetCursorWorldPosition()
    {
        Vector3 cursorWorldPos = Vector3.zero;

        if (Physics.Raycast(_mouseRay, out RaycastHit hitInfo, Mathf.Infinity, _groundLayerMask))
        {
            cursorWorldPos = hitInfo.point;
        }

        return cursorWorldPos;
    }

    private void CalculateForce()
    {
        Vector3 forceDirection = (GetCursorWorldPosition() - transform.position);
        ProjectileForce = forceDirection * _propellingForce;
        ProjectileForce.y = 0;
    }
}
