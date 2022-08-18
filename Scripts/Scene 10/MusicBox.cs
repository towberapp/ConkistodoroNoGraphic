using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicBox : AnimatorObject
{
    private int Left = 0,
                Right = 0;

    [SerializeField]
    private int _CorrectLeft = 3,
                _CorrectRight = 2;

    [SerializeField]
    private UnityEvent _CompleteEvent = null;

    

    private void Check()
    {
        if (Left == _CorrectLeft && Right == _CorrectRight)
            _CompleteEvent.Invoke();
    }

    public void TouchRight()
    {
        Right = Right == 3 ? 0 : Right + 1;
        animator.SetInteger(nameof(Right), Right);
        Check();
    }

    public void TouchLeft()
    {
        Left = Left == 3 ? 0 : Left + 1;
        animator.SetInteger(nameof(Left), Left);
        Check();
    }
}
