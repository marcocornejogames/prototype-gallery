using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Name, Date
public class ThirdPersonCamera : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private CinemachineFreeLook _camera;

	[Header("Customization")]
	[SerializeField] private bool _customBool;

	[Header("Feedback")]
	[SerializeField] private int _feedbackInt;

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

	//Event Calls _______________________________________________
	public void UpdateCameraAxis(Vector2 input)
	{
		_camera.m_YAxis.m_InputAxisValue = input.y;
		_camera.m_XAxis.m_InputAxisValue = input.x;
	}
}
