using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Name, Date
public class ThirdPersonCharacter : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private MonoBehaviour _componentReference;

	[Header("Customization")]
	[SerializeField] private bool _customBool;

	[Header("Feedback")]
	[SerializeField] private int _feedbackInt;

	//Unity Messages ______________________________________________
	void Start()
    	{
    	}

   	void Update()
	{
		OnUpdate();
    	}

	//Custom Methods _______________________________________________
	private void OnUpdate()
	{
	}
}
