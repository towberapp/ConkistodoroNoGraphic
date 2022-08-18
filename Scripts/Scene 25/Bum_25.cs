using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Bum_25 : AnimatorObject
{
    private enum BumState
    {
        Down,
        Forward,
        Backstep,
        Attack,
        Finished
    }
    [Header("Path Params")]
    [SerializeField] private MapPoint _StartPoint;
    [SerializeField] private MapPoint _EndPoint;
    [SerializeField] private PointSystem _PointSystem;
    [Header("Move Params")]
    [SerializeField] private float _ForwardSpeed;
    [SerializeField] private float _BackSpeed;
    [SerializeField] private float _AfterAttackTime;
    [SerializeField] private float _BackstepTime;
    [Header("Finish Settings")]
    [SerializeField] private UnityEvent _DoneEvent;
    private int State = 0; //Anim State
    private bool IsAttack = false;
    private Coroutine _Movement;
    private BumState _BumState = BumState.Down;

    public void GoElevator() => animator.SetInteger(nameof(State), State = 1);
    public void GoUp() => animator.SetInteger(nameof(State), State = 2);
    public void Finish()
    {
        _BumState = BumState.Finished;
        transform.position = _EndPoint.Position;
        transform.localScale = _EndPoint.TargetScale * Vector3.one;
        animator.SetInteger(nameof(State), State = 3);
    }

    public void AttackPers() => animator.SetBool(nameof(IsAttack), IsAttack = true);
    public void PauseAttack() => animator.SetBool(nameof(IsAttack), IsAttack = false);


    private IEnumerator MoveForward()
    {
        Path currentPath = _PointSystem.GetPath(transform.position, _EndPoint);
        MapPoint currentPoint = currentPath.CurrentPoint;

        animator.Play("Forward");
        while (transform.position != _EndPoint.transform.position)
        {
            if (_StartPoint.Position.x > transform.position.x)
                transform.position = _StartPoint.Position;
            if (currentPoint.Position.x <= transform.position.x)
                currentPoint = currentPath.GetNextPoint();
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.Position,
                                                        Time.deltaTime * _ForwardSpeed);
            yield return null;
        }
        _DoneEvent.Invoke();
    }
    public void SetUp()
    {
        GoUp();
        transform.position = _StartPoint.transform.position;
        transform.localScale = Vector3.one * _StartPoint.TargetScale;
        _BumState = BumState.Forward;
        if (_Movement != null)
            StopCoroutine(_Movement);
        _Movement = StartCoroutine(MoveForward());
    }

    private IEnumerator MoveBack(float time)
    {
        Path currentPath = _PointSystem.GetPath(transform.position, _StartPoint);
        MapPoint currentPoint = currentPath.CurrentPoint;
        float startTime = Time.time;

        animator.Play("Back");
        while (Time.time - startTime < time && transform.position != _StartPoint.transform.position)
        {
            if (currentPoint.Position.x >= transform.position.x)
                currentPoint = currentPath.GetNextPoint();
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.Position,
                                                        Time.deltaTime * _BackSpeed);
            yield return null;
        }
        _BumState = BumState.Forward;
        _Movement = StartCoroutine(MoveForward());
    }

    public void Back()
    {
        if (_BumState != BumState.Forward)
            return;
        _BumState = BumState.Backstep;
        StopCoroutine(_Movement);
        _Movement = StartCoroutine(MoveBack(_BackstepTime));
    }

    public void StartAttack()
    {
        if (_BumState != BumState.Forward)
            return;
        _BumState = BumState.Attack;
        animator.Play("Attack Bug");
        if (_Movement != null)
            StopCoroutine(_Movement);
    }

    public void StopAttack()
    {
        if (_BumState != BumState.Attack)
            return;
        _BumState = BumState.Forward;
        if (_Movement != null)
            StopCoroutine(_Movement);
        _Movement = StartCoroutine(MoveForward());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.GetComponent<BugHand_25>() is var enemy && enemy)
            || (collision.GetComponent<Bug_25>() is var bug && bug && _BumState != BumState.Attack))
        {
            if (_Movement != null)
                StopCoroutine(_Movement);
            _BumState = BumState.Backstep;
            _Movement = StartCoroutine(MoveBack(_AfterAttackTime));
        }
    }
}
