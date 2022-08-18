using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bum12 : AnimatorObject
{
    private bool IsLeft = true,
                IsCatted = false;

    public void SetLeft()
    {
        IsLeft = true;
        animator.SetBool(nameof(IsLeft), IsLeft);
    }
    public void SetRight()
    {
        IsLeft = false;
        animator.SetBool(nameof(IsLeft), IsLeft);
    }
    public void SetCat()
    {
        IsCatted = true;
        animator.SetBool(nameof(IsCatted), IsCatted);
    }
}
