using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatiskafIniter_19 : MonoBehaviour
{
    [SerializeField] private Batiskaf_19 _Batiskaf;
    [SerializeField] private BatiskafController_19 _Remote;
    private bool _IsPointInited = false;
    private bool _IsRopeInited = false;

    private IEnumerator InitingRope(int value)
    {
        while (!_IsPointInited)
            yield return null;
        _Batiskaf.SetRope(value);
        _Remote.Appear();
        _IsRopeInited = true;
    }

    public void InitPoint(Point_19 point)
    {
        _Batiskaf.SetToPoint(point);
        _IsPointInited = true;
    }

    public void InitRope(int ropeId)
    {
        StartCoroutine(InitingRope(ropeId));
    }

}
