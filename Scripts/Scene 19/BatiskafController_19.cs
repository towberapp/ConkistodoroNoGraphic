using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatiskafController_19 : AnimatorObject
{
    private int Rope;
    private bool IsAppear = false;
    private bool IsDisappear = false;
    

    public void SetRope(int ropeId)
    {
        animator.SetInteger(nameof(Rope), Rope = ropeId);
    }

    public void Animated()
    {
        animator.SetBool(nameof(IsAppear), IsAppear = false);
        animator.SetBool(nameof(IsDisappear), IsDisappear = false);
    }

    public void Appear()
    {
        animator.SetBool(nameof(IsAppear), IsAppear = true);
    }

    public void Disappear()
    {
        animator.SetBool(nameof(IsDisappear), IsDisappear = true);
    }
}
