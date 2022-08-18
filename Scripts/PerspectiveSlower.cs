using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveSlower : MonoBehaviour
{
    [SerializeField]
    private Vector2 _TargetSpeed = Vector2.zero;

    public Vector2 TargetSpeed { 
        get
        {
            return _TargetSpeed;
        }
    }
}
