using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyField_27 : AnimatorObject
{
    private int State = 0;
    private bool IsOff = false;

    public void Disable() => animator.SetBool(nameof(IsOff), IsOff = true);
    public void SetBiggest() => animator.SetInteger(nameof(State), State = 0);
    public void SetBig() => animator.SetInteger(nameof(State), State = 1);
    public void SetSmall() => animator.SetInteger(nameof(State), State = 2);
    public void SetSmallest() => animator.SetInteger(nameof(State), State = 3);
}
