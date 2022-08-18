using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObjectFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _Target = null;

    [SerializeField]
    private bool _IsPos = true,
        _IsRot = true,
        _isScale = true;

    void Update()
    {
        if (_IsPos && _Target && _Target.position != transform.position)
            transform.position = _Target.position;
        if (_IsRot && _Target && _Target.eulerAngles != transform.eulerAngles)
            transform.eulerAngles = _Target.eulerAngles;
        if (_isScale && _Target && _Target.localScale != transform.localScale)
            transform.localScale = _Target.localScale;
    }
}
