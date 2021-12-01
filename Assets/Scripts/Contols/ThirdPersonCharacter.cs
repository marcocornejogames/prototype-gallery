using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

// Marco Cornejo, November 18th 2021
public class ThirdPersonCharacter : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private CharacterMovement _characterMovement;
	[SerializeField] private CharacterStats _characterStats;

	[Header("Customization")]
	[SerializeField] private float _maxHealh = 100;
	[SerializeField] private float _healthRegenRate = 0.2f;
	[SerializeField] private float _maxStamina = 100;
	[SerializeField] private float _staminaRegenRate = 0.2f;
	[SerializeField] private float _sprintStaminaCost = 1f;


	[Header("Unity Events")]
	[SerializeField] private FloatEvent _healthEvent;
	[SerializeField] private FloatEvent _staminaEvent;

    //Unity Messages ______________________________________________
    private void Awake()
    {
		//Set Character Stats _____________________________________
		_characterStats.MaxHealth = _maxHealh;
		_characterStats.CurrentHealth = _maxHealh;
		_characterStats.HealthRegenRate = _healthRegenRate;

		_characterStats.MaxStamina = _maxStamina;
		_characterStats.CurrentStamina = _maxStamina;
		_characterStats.StaminaRegenRate = _staminaRegenRate;

		//Character Movement _______________________________________
		_characterMovement = GetComponent<CharacterMovement>();
    }
    void Start()
    {

    }

   	void Update()
	{
		//Manage stamina economy _____________________________________
		CalculateStamina();

		//Update Stats ________________________________________
		_characterStats.UpdateCharacterStats();
		BroadcastCharacterStats();
    }

	//Custom Methods _______________________________________________
	private void BroadcastCharacterStats()
    {
		_healthEvent.Invoke(_characterStats.CurrentHealth / _characterStats.MaxHealth);
		_staminaEvent.Invoke(_characterStats.CurrentStamina / _characterStats.MaxStamina);
    }

	private void CalculateStamina()
    {
		if(_characterMovement.CurrentMovementMode == CharacterMovement.MovementMode.Sprinting)
        {
			_characterStats.StaminaRegen = false;
			_characterStats.CurrentStamina -= _sprintStaminaCost;

			if(_characterStats.CurrentStamina <= 0)
            {
				_characterStats.CurrentStamina = 0;
				_characterMovement.Trip();
            }
        }
		else
        {
			_characterStats.StaminaRegen = true;
        }
    }
}
