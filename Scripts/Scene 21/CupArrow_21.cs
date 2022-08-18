using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupArrow_21 : AnimatorObject
{
    [SerializeField] private Vector3 _RightButtonDirection;
    [SerializeField] private Vector3 _LeftButtonDirection;

    [SerializeField] private float _MaxRot;
    [SerializeField] private float _RotDuration;
    [SerializeField] private AnimationCurve _RotationCurve;

    private Rigidbody2D _Rigidbody;
    [SerializeField]private int _CollisionsCount = 0;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    private IEnumerator FalseRotating()
    {
        float progress = 0;
        float startTime = Time.time;
        float prevValue = 0;

        while (progress <= 1)
        {
            _Rigidbody.rotation -= prevValue;
            _Rigidbody.rotation += (prevValue = _RotationCurve.Evaluate(Time.time - startTime));
            progress = (Time.time - startTime) / _RotDuration;
            yield return null;
        }
    }

    public void MoveRight(float strength)
    {
        _Rigidbody.AddForce(_RightButtonDirection * strength, ForceMode2D.Impulse);
    }

    public void MoveLeft(float strength)
    {
        _Rigidbody.AddForce(_LeftButtonDirection * strength, ForceMode2D.Impulse);
    }

    public bool Check()
    {
        if (_CollisionsCount > 0)
        {
            StartCoroutine(FalseRotating());
            return false;
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var arrow = collision.GetComponent<CupArrow_21>();
        if (arrow)
            _CollisionsCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var arrow = collision.GetComponent<CupArrow_21>();
        if (arrow)
            _CollisionsCount--;
    }
}
