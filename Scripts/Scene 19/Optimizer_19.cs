using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optimizer_19 : MonoBehaviour
{
    [SerializeField] private bool _startState = true;
    private Rigidbody2D[] _rigids;
    void Start()
    {
        _rigids = GetComponentsInChildren<Rigidbody2D>();
        foreach (var rigid in _rigids)
            if (!_startState)
                rigid.isKinematic = !_startState;
    }
}
