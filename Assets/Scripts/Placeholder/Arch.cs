using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Name, Date
public class Arch : MonoBehaviour, iHoverable
{
    public void Hover(bool isHovering)
    {
        if(isHovering) Debug.Log("Cursor is hovering arch");
    }
}
