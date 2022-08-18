using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSetter : MonoBehaviour
{
    [SerializeField]
    private Transform _Target = null;

    public void SetToTarget()
    {
        transform.position = _Target.transform.position;
    }
}
