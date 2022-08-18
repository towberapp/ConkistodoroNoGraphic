using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SideFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _Target = null;

    [SerializeField]
    private Transform _TeloChild = null;

    [SerializeField]
    private bool _IsFollow = false;
    public void SetToTarget()
    {
        transform.position = _Target.position;
    }

    private void Update()
    {
        if (_IsFollow)
            SetToTarget();
    }
}
