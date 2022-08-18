using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lamp_36 : MonoBehaviour
{
    [SerializeField] private Transform _lightCircle;
    [SerializeField] private Animator _lightAnimator;
    [Header("Noise Settings")]
    [SerializeField] private float _noiseSpeed = 1;
    [SerializeField] private float _scaleX = 1;
    [SerializeField] private float _scaleY = 1;
    [Header("Follow Settings")]
    [SerializeField] private float _followSpeed = 1;
    [SerializeField] private float _changeTime = 0.5f;
    [SerializeField] private Rigidbody2D _floor;
    [Header("Up Settings")]
    [SerializeField] private Animator _ButtonL;
    [SerializeField] private Animator _ButtonR;
    [SerializeField] private Pers_36 _pers;
    [SerializeField] private Transform _endPos;
    [SerializeField] private Transform _startPos;
    [SerializeField] private float _gravityScale = 5; 
    [SerializeField] private float _minForce = 1;
    [SerializeField] private float _maxForce = 3;
    [SerializeField] private float _rotSpeed = 0.5f;
    [SerializeField] private float _extraSpeed = 1;
    [SerializeField] private float _maxAngle = 75;
    [SerializeField] private UnityEvent _finishEvent;
    [SerializeField] private UnityEvent _fallEvent;
    private int _extraSpeedDirection = 0;
    private float _rotateDirection = -1;
    private bool IsBoard = false;
    [SerializeField] private bool _isAnchorChanged;
    private Rigidbody2D _rigidbody;
    private DistanceJoint2D _distanceJoint;
    private float _startDeltaX;
    private float _deltaX = 0;
    private Coroutine _distanceChange;
    private Coroutine _goingUp;

    private Rigidbody2D rigidbody
    {
        get
        {
            if (!_rigidbody)
                _rigidbody = GetComponent<Rigidbody2D>();
            return _rigidbody;
        }
    }

    private DistanceJoint2D distanceJoint
    {
        get
        {
            if (!_distanceJoint)
                _distanceJoint = GetComponent<DistanceJoint2D>();
            return _distanceJoint;
        }
    }

    public void SetBoard() => _lightAnimator.SetBool(nameof(IsBoard), IsBoard = true);

    private IEnumerator DecreaseVelocity()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForFixedUpdate();
            rigidbody.velocity = new Vector2(0, 0);
        }
    }

    public void ChangeConnectedAnchor(float duration) => StartCoroutine(ChangeAnchor(duration));

    private IEnumerator ChangeAnchor(float duration)
    {
        float progress = 0;
        float time = Time.time;
        Vector2 startAnchor = distanceJoint.connectedAnchor;
        Vector2 targetAnchor = new Vector2(0.29f, -3.07f);
        while (progress < 1 && !_isAnchorChanged)
        {
            progress = (Time.time - time) / duration;
            distanceJoint.connectedAnchor = startAnchor + (targetAnchor - startAnchor) * progress;
            yield return new WaitForFixedUpdate();
        }
        distanceJoint.connectedAnchor = targetAnchor;
        rigidbody.velocity *= 0;
        _isAnchorChanged = true;
    }

    public void SetMoved()
    {
        transform.position = _startPos.position + Vector3.up * distanceJoint.distance;
        _isAnchorChanged = true;
        distanceJoint.connectedAnchor = new Vector2(0.29f, -3.07f);
        rigidbody.velocity *= 0;
    }

    private void Start()
    {
        _startDeltaX = rigidbody.position.x - _floor.position.x;
        _deltaX = _startDeltaX;
        StartCoroutine(DeltaChanger());
        StartCoroutine(Noise());
        StartCoroutine(DecreaseVelocity());
    }

    public void TurnRight()
    {
        _pers.TurnRight();
        _extraSpeedDirection = -1;
    }

    public void StopTurning()
    {
        _pers.StopTurning();
        _extraSpeedDirection = 0;
    }

    public void TurnLeft()
    {
        _pers.TurnLeft();
        _extraSpeedDirection = 1;
    }

    public void Reset()
    {
        rigidbody.gravityScale = -_gravityScale;
        _pers.Reset();
        _pers.gameObject.SetActive(false);
        _lightCircle.eulerAngles = Vector3.zero;
        distanceJoint.enabled = true;
        StopTurning();
        StopUp();
    }

    public void GoingUp()
    {
        if (_goingUp != null)
            StopCoroutine(_goingUp);
        _goingUp = StartCoroutine(Up());
    }

    public void StopUp()
    {
        if (_goingUp != null)
            StopCoroutine(_goingUp);
        _ButtonL.Play("Disappear");
        _ButtonR.Play("Disappear");
        rigidbody.gravityScale = 0;
        rigidbody.velocity *= 0;
    }

    public void Finish(float duration)
    {
        StopUp();
        StartCoroutine(RotateTo(0, duration, () => _finishEvent.Invoke()));
    }

    private IEnumerator RotateTo(float target, float duration, Action endAction)
    {
        float time = Time.time;
        Vector3 rotation = _lightCircle.eulerAngles;
        Vector3 startRot = rotation;
        float progress = 0;
        if (target == 0 && startRot.z > 180)
            target = 360;
        while (progress < 1)
        {
            progress = (Time.time - time) / duration;
            rotation.z = startRot.z + (target - startRot.z) * progress;
            _lightCircle.eulerAngles = rotation;
            yield return null;
        }
        endAction.Invoke();
    }

    private IEnumerator Up()
    {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        rigidbody.gravityScale = _gravityScale;
        rigidbody.velocity = new Vector2(0, 0);
        _pers.gameObject.SetActive(true);
        distanceJoint.enabled = false;
        _ButtonL.Play("Appear");
        _ButtonR.Play("Appear");
        while (true)
        {
            rigidbody.AddForce(Vector3.up * UnityEngine.Random.Range(_minForce, _maxForce), ForceMode2D.Force);
            if (_lightCircle.eulerAngles.z > 360 - _maxAngle && _rotateDirection > 0 || _lightCircle.eulerAngles.z < 360 - _maxAngle && _rotateDirection < 0)
                _rotateDirection *= -1;
            _lightCircle.eulerAngles += Vector3.forward * (_rotateDirection * _rotSpeed + _extraSpeed * _extraSpeedDirection);
            if (Mathf.Abs(_lightCircle.eulerAngles.z) > _maxAngle && _lightCircle.eulerAngles.z < 360 - _maxAngle)
            {
                rigidbody.gravityScale = 0;
                rigidbody.velocity *= 0;
                _fallEvent.Invoke();
                StopCoroutine(_goingUp);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator DeltaChanger()
    {
        while (true)
        {
            _deltaX = rigidbody.position.x - _floor.position.x;
            _deltaX += (_startDeltaX - _deltaX) * Time.deltaTime * _followSpeed;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Noise()
    {
        Vector3 startPos = _lightCircle.localPosition;
        while (true)
        {
            Vector3 pos = _lightCircle.localPosition;
            pos.x = startPos.x + _scaleX * (Mathf.PerlinNoise(Time.time * _noiseSpeed, 0) - 0.5f);
            pos.y = startPos.y + _scaleY * (Mathf.PerlinNoise(0, Time.time * _noiseSpeed) - 0.5f);
            _lightCircle.localPosition = pos;
            yield return null;
        }
    }

    private IEnumerator MoveToDistance(float distance)
    {
        float startTime = Time.time;
        float deltaDistance = distance - distanceJoint.distance;
        float startDistance = distanceJoint.distance;
        float progress = 0;
        while (progress < 1)
        {
            progress = Time.time - startTime / _changeTime;
            distanceJoint.distance = startDistance + deltaDistance * progress;
            yield return new WaitForFixedUpdate();
        }
        distanceJoint.distance = startDistance + deltaDistance;
    }

    public void SetDistance(float distance)
    {
        if (_distanceChange != null)
            StopCoroutine(_distanceChange);
        _distanceChange = StartCoroutine(MoveToDistance(distance));
    }

    public void SetInstantDistance(float distance)
    {
        if (_distanceChange != null)
            StopCoroutine(_distanceChange);
        distanceJoint.distance = distance;
    }
}
