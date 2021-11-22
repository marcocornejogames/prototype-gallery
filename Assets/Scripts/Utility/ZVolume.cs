using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marco Cornejo, November 17th 2021
[RequireComponent(typeof(BoxCollider))]
public class ZVolume : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private BoxCollider _collider;

    //Unity Messages ______________________________________________
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
    }
}
