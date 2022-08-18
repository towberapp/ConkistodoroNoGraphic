using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ScaleCalculator : MonoBehaviour
{
    [SerializeField]
    private float _StartScale = 1,
                _EndScale = 2;

    [SerializeField]
    private Vector2 _CheckDirection = Vector2.zero; //Чтобы указать указать в какую сторону идет скалирование

    private Vector2 _StartPos = Vector2.zero, 
                    _EndPos = Vector2.zero;

    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector2 size = collider.size;
        if (_CheckDirection.x != 0)
            _CheckDirection.x /= Mathf.Abs(_CheckDirection.x); // Защита от дебила,который поставить не числа 1 или 0
        if (_CheckDirection.y != 0)
            _CheckDirection.y /= Mathf.Abs(_CheckDirection.y); // Защита от дебила,который поставить не числа 1 или 0
        size.x *= _CheckDirection.x;
        size.y *= _CheckDirection.y;
        _StartPos = (Vector2)transform.position + collider.offset - size / 2;
        _EndPos = _StartPos + size;
    }

    public float GetScale(Vector2 pos)
    {
        pos -= _StartPos;
        pos.x *= _CheckDirection.x; 
        pos.y *= _CheckDirection.y;
        float deltaScale = pos.magnitude / (_EndPos - _StartPos).magnitude * Mathf.Abs(_StartScale - _EndScale);
        return _StartScale < _EndScale ? _StartScale + deltaScale : _StartScale - deltaScale;
    }
}
