using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pers_25 : AnimatorObject
{
    [SerializeField] private TimelineManager _timelineManger;
    [SerializeField] private TimelineStart _waitTimeline;
    [SerializeField] private InteractableObject _knifeThrowingWaiter;
    [SerializeField] private InteractableObject _knifeThrowing;

    private int State = 0;
    private bool IsAttack = true;

    public void StopWaiting()
    {
        _timelineManger.RemoveFromQueue(_waitTimeline.Get());
        _knifeThrowing.gameObject.SetActive(true);
        _knifeThrowingWaiter.gameObject.SetActive(false);
    }

    public void StartWaiting()
    {
        _knifeThrowing.gameObject.SetActive(false);
        _knifeThrowingWaiter.gameObject.SetActive(true);
    }

    public void Die() => animator.SetInteger(nameof(State), State = 1);
    public void FlagTaken() => animator.SetInteger(nameof(State), State = 2);

    public void StopAttack() => animator.SetBool(nameof(IsAttack), IsAttack = false);

    public void Attack() => animator.SetBool(nameof(IsAttack), IsAttack = true);
}
