using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marco, November 14th
public class HighlightEffect : MonoBehaviour, iHoverable
{
    [Header("Component References")]
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _highlightMaterial;

    [Header("Customization")]
    [SerializeField] private int _materialsListIndex = 1;
    [SerializeField] private float _maxHighlightStrength = 1;
    [SerializeField] private float _minHighlightStrength = 0;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {

    }
    public void Hover(bool isHovering)
    {
        if(isHovering) Debug.Log("Cursor is hovering arch");

        _meshRenderer.materials[_materialsListIndex].SetFloat("_strength", (isHovering? _maxHighlightStrength : _minHighlightStrength));
    }
}
