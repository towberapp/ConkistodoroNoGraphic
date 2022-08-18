using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Pers_36 : AnimatorObject
{
    [SerializeField] private float _fallSpeed = 1;
    [SerializeField] private UnityEvent _finishEvent;
    [SerializeField] private UnityEvent _groundEvent;
    private Vector3 _startPos;
    private UnityEngine.U2D.IK.IKManager2D[] _IKManagers;
    private Rigidbody2D _rigidbody;
    private int Direction = 0;
    private bool _isFalling = false;

    private void Start()
    {
        _IKManagers = GetComponentsInChildren<UnityEngine.U2D.IK.IKManager2D>();
        _startPos = transform.localPosition;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        _isFalling = false;
        _rigidbody.gravityScale = 0;
        foreach (var manager in _IKManagers)
            manager.enabled = true;
    }

    public void FixedUpdate()
    {
        if (!_isFalling)
            transform.localPosition = _startPos;
    }

    public void Fall()
    {
        _isFalling = true;
        _rigidbody.gravityScale = _fallSpeed;
        foreach (var manager in _IKManagers)
            manager.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FlyFinish_36>() is var finish && finish)
            _finishEvent.Invoke();
        else if (collision.GetComponent<Ground>() is var ground && ground)
            _groundEvent.Invoke();
    }

    public void TurnRight() => animator.SetInteger(nameof(Direction), Direction = 1);
    public void StopTurning() => animator.SetInteger(nameof(Direction), Direction = 0);
    public void TurnLeft() => animator.SetInteger(nameof(Direction), Direction = -1);
}
