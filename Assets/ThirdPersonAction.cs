using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonAction : MonoBehaviour
{
    [SerializeField] private ThirdPersonAnimation _animationController;
    [SerializeField] private CharacterMovement _characterMovement;
    private int currentAttackType = 0;

    private void Awake()
    {
        _animationController = GetComponentInChildren<ThirdPersonAnimation>();
        _characterMovement = GetComponent<CharacterMovement>();
    }
    public void TryAction()
    {
        if (!_characterMovement.CanMove) return;
        if (currentAttackType == 0) currentAttackType = 1;
        else currentAttackType = 0;
        _animationController.SetActionType(0);
        _animationController.SetAttackType(currentAttackType);
        _animationController.OnAction();
    }
}
