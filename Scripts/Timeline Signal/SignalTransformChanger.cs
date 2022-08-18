using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTransformChanger : MonoBehaviour
{
    [SerializeField]
    private Vector3 _Position = Vector3.zero;

    public float PosX
    {
        get
        {
            return _Position.x;
        }
        set
        {
            _Position.x = value;
        }
    }
    public float PosY
    {
        get
        {
            return _Position.y;
        }
        set
        {
            _Position.y = value;
        }
    }

    public float PosZ
    {
        get
        {
            return _Position.z;
        }
        set
        {
            _Position.z = value;
        }
    }

    [SerializeField]
    private Vector3 _Rotation = Vector3.zero;

    public float RotX
    {
        get
        {
            return _Rotation.x;
        }
        set
        {
            _Rotation.x = value;
        }
    }
    public float RotY
    {
        get
        {
            return _Rotation.y;
        }
        set
        {
            _Rotation.y = value;
        }
    }
    public float RotZ
    {
        get
        {
            return _Rotation.z;
        }
        set
        {
            _Rotation.z = value;
        }
    }
    private void Start()
    {
        _Position = transform.localPosition;
        _Rotation = transform.localEulerAngles;
    }

    
    private void Update()
    {
        if (transform.localPosition != _Position)
            transform.localPosition = _Position;

        if (transform.localEulerAngles != _Rotation)
        {
            transform.localEulerAngles = _Rotation;
        }
    }
}
