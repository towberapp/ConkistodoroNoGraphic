using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitingEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _SuccessEvent = null, _FailEvent = null;

    [SerializeField]
    private float _WaitTime = 0;

    protected delegate bool CheckFunc();
    protected IEnumerator Waiting(CheckFunc check)
    {
        while (!check())
            yield return null;

        float time = Time.time;
        while (Time.time - time < _WaitTime && check())
            yield return null;
        if (Time.time - time >= _WaitTime)
            _SuccessEvent.Invoke();
        else
        {
            yield return new WaitForSeconds(0.5f);
            _FailEvent.Invoke();
        }
    }
    
    protected void StartWaiting(CheckFunc f)
    {
        StopAllCoroutines();
        StartCoroutine(
            Waiting(f)
        );
    }

    protected void StartWaiting()
    {
        StopAllCoroutines();
        StartCoroutine(
            Waiting(() => true)
        );
    }
}
