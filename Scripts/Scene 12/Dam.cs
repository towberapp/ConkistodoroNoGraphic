using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dam : AnimatorObject
{
    private int Lever = 0;
    private bool IsFalling = false;

    public void SetLever(int newLever)
    {
        Lever = newLever;
        animator.SetInteger(nameof(Lever), Lever);
    }

    public void Fall()
    {
        IsFalling = true;
        animator.SetBool(nameof(IsFalling), IsFalling);
    }
}
