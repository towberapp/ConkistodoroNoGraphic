using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor_27 : AnimatorObject
{
    [SerializeField] private int State = 0;
    [SerializeField] private int _CorrectState = 0;

    public bool IsCorrect
    {
        get => State == _CorrectState;
    }

    private void Start()
    {
        animator.SetInteger(nameof(State), State);
    }

    public void Open() => animator.SetInteger(nameof(State), State = _CorrectState);
    public void Reset() => animator.SetInteger(nameof(State), State = 0);

    public void MoveRight() => animator.SetInteger(nameof(State), State = 1);
    public void MoveLeft() => animator.SetInteger(nameof(State), State = -1);
}
