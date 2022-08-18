using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_19 : MonoBehaviour
{
    [SerializeField] private RopePart_19[] _Parts;
    [SerializeField] private bool _isAttractive = false;

    public bool IsAttractive
    {
        get => _isAttractive;
        set
        {
            if (_isAttractive == value)
                return;
            foreach (var part in _Parts)
                part.IsAttractive = value;
            _isAttractive = value;
        }
    }

    private void Start()
    {
        _Parts = GetComponentsInChildren<RopePart_19>(true);
    }
}
