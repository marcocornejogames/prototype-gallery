using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Marco, November 13th 2021
public class GameEventListener<T> : MonoBehaviour
{
	[Header("Component References")]
	[SerializeField] private  GameEvent<T> _triggerEvent;

	[Header("Customization")]
	[SerializeField] private UnityEvent<T> _broadcastEvent;

    //Unity Messages ______________________________________________________
    private void Awake()
    {
        _triggerEvent.Event.AddListener(Invoke);
    }

    //Custom Methods ______________________________________________________
    private void Invoke(T parameter)
    {
        _broadcastEvent.Invoke(parameter);
    }
}
