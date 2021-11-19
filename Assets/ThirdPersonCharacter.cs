using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marco Cornejo, November 18th 2021
public class ThirdPersonCharacter : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private CharacterMovement _characterMovement;

	[Header("Customization")]
	[SerializeField] private bool _customBool;

	[Header("Feedback")]
	[SerializeField] private Vector2 _movementInput;

	//Unity Messages ______________________________________________
	void Start()
    {
    }

   	void Update()
	{

    }

	//Custom Methods _______________________________________________

}
