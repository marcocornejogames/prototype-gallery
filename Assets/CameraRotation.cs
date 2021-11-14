using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Marco, November 13th
public class CameraRotation : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private CinemachineFreeLook _camera;

	[Header("Feedback")]
	[SerializeField] private float _rotationInput;

	//Unity Messages ______________________________________________
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
	}
    public void UpdateRotationInput(float input)
    {
        _rotationInput = input;
    }
}
