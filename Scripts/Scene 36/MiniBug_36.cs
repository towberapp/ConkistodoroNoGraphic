using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniBug_36 : AnimatorObject
{
    [SerializeField] private Animator _lid;
    [SerializeField] private float _WaitTime = 30;
    [SerializeField] private UnityEvent _DoneEvent;
    private bool IsRun = false;


    public void TouchLid() => _lid.Play("Touched");

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(_WaitTime);
        _DoneEvent.Invoke();
    }

    public void Run() => animator.SetBool(nameof(IsRun), IsRun = true);

    public void Disable() => gameObject.SetActive(true);

    private void Start()
    {

        StartCoroutine(Waiter());
    }
}
