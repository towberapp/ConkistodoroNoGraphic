using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batiskaf_16 : AnimatorObject
{
    private bool IsPulled = false,
           IsOpened = false, IsRoped = false;

    public void Pull()
    {
        IsPulled = true;
        animator.SetBool(nameof(IsPulled), IsPulled);
    }

    public void SetRope()
    {
        IsRoped = true;
        animator.SetBool(nameof(IsRoped), IsRoped);
    }

    public void Open()
    {
        IsOpened = true;
        animator.SetBool(nameof(IsOpened), IsOpened);
    }
}
