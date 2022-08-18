using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField]
    private bool _State = true;

    private bool _PrevState = true;

    private Collider2D[] _Colliders;
    // Start is called before the first frame update
    void Start()
    {
        _PrevState = !_State;
        _Colliders = Array.FindAll<Collider2D>(GetComponentsInChildren<Collider2D>(true), col => col.isTrigger == true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_PrevState != _State)
        {
            foreach (var col in _Colliders)
                col.enabled = _State;
            _PrevState = _State;
        }
    }
}
