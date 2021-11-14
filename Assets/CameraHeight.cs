using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Marco, November 13th
public class CameraHeight : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private CinemachineFreeLook _camera;

    [Header("Feedback")]
    [SerializeField] private float _rotationInput;

    //Unity Messages ______________________________________________
    private void Awake()
    {
        _camera = GetComponent<CinemachineFreeLook>();
    }
    void Start()
    {
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        ApplyRotation();
    }

    //Custom Methods _______________________________________________
    private void ApplyRotation()
    {
        _camera.m_XAxis.m_InputAxisValue = _rotationInput;
    }
    public void UpdateRotationInput(float input)
    {
        _rotationInput = input;
    }
}
