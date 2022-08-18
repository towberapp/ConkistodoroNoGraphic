using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Termometer_18 : AnimatorObject
{
    private bool IsBroken = false,
                IsBooted = false,
                IsTaken = false;

    public void SetBroken()
    {
        IsBroken = true;
        animator.SetBool(nameof(IsBroken), IsBroken);
    }

    public void SetTaken()
    {
        IsTaken = true;
        animator.SetBool(nameof(IsTaken), IsTaken);
    }

    public void SetBooted()
    {
        IsBooted = true;
        animator.SetBool(nameof(IsBooted), IsBooted);
    }
}
