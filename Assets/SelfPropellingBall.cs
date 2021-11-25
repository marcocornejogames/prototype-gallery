using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPropellingBall : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _propellingForce = 10f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private bool _predictPath = true;
    private Ray _mouseRay;

    public Vector3 ProjectileForce;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _projectile = GetComponent<Projectile>();


    }
    public void UpdateCursorRay(Ray ray)
    {
        _mouseRay = ray;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        CalculateForce();
        if(_predictPath) _projectile.PredictTrajectory(ProjectileForce);
    }
    public void OnClick(bool onClick)
    {
        if (!onClick)
        {
            _predictPath = true;
            _projectile.TogglePredictionRendering(_predictPath);
            return;
        }
        _predictPath = false;

        ApplyForce();
        _projectile.TogglePredictionRendering(_predictPath);
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
        ProjectileForce.y = transform.position.y;
    }
}
