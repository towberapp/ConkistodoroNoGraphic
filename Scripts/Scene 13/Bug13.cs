using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug13 : AnimatorObject
{
    private bool IsTrapped = false,
                IsFeared = false,
                IsGroveOpened = false;

    [SerializeField]
    private State _BugState = null;
    public void SetFear(bool value)
    {
        IsFeared = value;
        animator.SetBool(nameof(IsFeared), IsFeared);
        _BugState.Value = value ? "Box" : "Out";
    }

    public void CheckTrap()
    {
        if (IsTrapped)
            _BugState.Value = "Trapped";
    }
    public void SetTrap()
    {
        IsTrapped = true;
        animator.SetBool(nameof(IsTrapped), IsTrapped);
    }

    public void SetGrove(bool value)
    {
        IsGroveOpened = value;
        animator.SetBool(nameof(IsGroveOpened), IsGroveOpened);
    }
}
