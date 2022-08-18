using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    [SerializeField]
    private float _Scale = 1;
    private float Scale
    {
        get
        {
            return _Scale;
        }
        set
        {
            _Scale = value;
            if (_StartScale.x < 0)
                _StartScale = transform.localScale;
            transform.localScale = _StartScale * _Scale;
        }
    }

    private Vector3 _StartScale = new Vector3(-1, -1, -1);
    private float _TargetScale = 0;
    private float _MaxDeltaScale = 0;

    public float TargetScale
    {
        get
        {
            return _TargetScale;
        }
        set
        {
            _TargetScale = value;
            _MaxDeltaScale = _TargetScale - _Scale;
        }
    }

    public void ScaleToTarget()
    {
        Scale = TargetScale;
    }
    
    public float CalcNewScale(float maxDistance, float deltaPos)
    {
        float deltaScale =  _MaxDeltaScale * (deltaPos / maxDistance);
        Scale += deltaScale;
        return Scale;
    }
}
