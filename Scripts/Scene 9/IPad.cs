using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class IPad : AnimatorObject
{
    [SerializeField]
    private Transform _BackSprite = null;

    [SerializeField]
    private float _MoveSpeed = 1;

    [SerializeField]
    private Vector2 _Angles = Vector2.zero;

    private bool IsRight = false,
                IsLeft = false,
                IsOn = false;

    [SerializeField]
    private PlayableDirector _Player = null;
    [SerializeField]
    private TimelineAsset _Appear = null;

    public Vector2 _Direction = Vector2.zero;


    public void StartPicture()
    {
        _Player.Play(_Appear);
        _BackSprite.localPosition = Vector2.zero;
    }

    private void Update()
    {
        if (IsOn)
            Move();
    }

    public void Move()
    {
        Vector2 newPos = _BackSprite.localPosition;
        newPos += (_Direction * _MoveSpeed * Time.deltaTime);
        if (newPos.x >= _Angles.x && newPos.x <= _Angles.y)
            _BackSprite.localPosition = newPos;
    }
    public void SetDirection(int x) => _Direction.x = x;
    public void SetLeft(bool state) => animator.SetBool(nameof(IsLeft), state);
    public void SetRight(bool state) => animator.SetBool(nameof(IsRight), state);
    public void SetOn() => animator.SetBool(nameof(IsOn), IsOn = true);
}
