using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPropellingBall : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _propellingForce = 10f;
    [SerializeField] private Rigidbody _rigidbody;
    private Ray _mouseRay;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void UpdateCursorRay(Ray ray)
    {
        _mouseRay = ray;
    }

    public void OnClick(bool onClick)
    {
        if (!onClick) return;
        ApplyForce();
    }
    private void ApplyForce()
    {
        Vector3 forceDirection = (GetCursorWorldPosition() - transform.position);
        _rigidbody.AddForce(forceDirection * _propellingForce);
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
}
