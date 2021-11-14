using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Marco, November 13th 2021
public class GameEvent<T> : ScriptableObject
{
    [Header("Customization")]
    public UnityEvent<T> Event;

    public void Invoke(T parameter)
    {
        Event.Invoke(parameter);
    }    
}
