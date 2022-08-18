using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate_16 : AnimatorObject
{
    private bool IsOn = false,
        IsKettle = false, IsKettleReady = false;

    public void SetState(bool newState)
    {
        IsOn = newState;
        animator.SetBool(nameof(IsOn), IsOn);
    }

    public void SwitchState()
    {
        IsOn = !IsOn;
        animator.SetBool(nameof(IsOn), IsOn);
    }
    public void SetKettle()
    {
        IsKettle = true;
        animator.SetBool(nameof(IsKettle), IsKettle);
    }

    public void SetKettleReady()
    {
        IsKettleReady = true;
        animator.SetBool(nameof(IsKettleReady), IsKettleReady);
    }
}
