using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Adventure Game/Character Stats")]
public class CharacterStats : ScriptableObject
{
    [Header("Character Customization")]
    public string CharacterName;

    [Header("Health")]
    public float MaxHealth;
    public float CurrentHealth;
    public float HealthRegenRate;
    public bool HealthRegen = true;

    [Header("Stamina")]
    public float MaxStamina;
    public float CurrentStamina;
    public float StaminaRegenRate;
    public bool StaminaRegen = true; 

    public void UpdateCharacterStats()
    {
        TryRegenHealth();
        TryRegenStamina();
    }

    private void TryRegenHealth()
    {
        if (!HealthRegen || CurrentHealth >= MaxHealth) return;
        CurrentHealth += HealthRegenRate;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }

    private void TryRegenStamina()
    {
        if (!StaminaRegen || CurrentStamina >= MaxStamina) return;
        CurrentStamina += StaminaRegenRate;
        if (CurrentStamina > MaxStamina) CurrentStamina = MaxStamina;
    }
}
