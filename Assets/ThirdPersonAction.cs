using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonAction : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private ThirdPersonAnimation _animationController;
    [SerializeField] private CharacterMovement _characterMovement;


    [Header("Customization")]
    [SerializeField] private float _minimumTimeBetweenActions = 1f;

    [Header("Feedback")]
    [SerializeField] private bool _canUseAction = true;

    //Action Type
    //Bare Hands = 0


    //Attack Type:
    //Left = 0
    //Right = 1

    private void Awake()
    {
        _animationController = GetComponentInChildren<ThirdPersonAnimation>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    public void TryActionRight()
    {
        TryAction(0, 1);
    }

    public void TryActionLeft()
    {
        TryAction(0, 0);
    }
    public void TryAction(int actionType, int actionHand)
    {
        if (!_characterMovement.CanMove || !_canUseAction) return;

        _canUseAction = false;
        Invoke("ResetCanUse", _minimumTimeBetweenActions);

        _animationController.SetActionType(actionType);
        _animationController.SetActionHand(actionHand);
        _animationController.OnAction();
    }

    private void ResetCanUse()
    {
        _canUseAction = true;
    }
}
