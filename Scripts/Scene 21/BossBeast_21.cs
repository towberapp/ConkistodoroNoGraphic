using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeast_21 : AnimatorObject
{
    private bool IsCup = true;
    private bool IsCoffee = true;
    private int Fish = 0;

    public void SetCup(bool state) => animator.SetBool(nameof(IsCup), IsCup = state);
    public void GiveKoffee() => animator.SetBool(nameof(IsCoffee), IsCoffee = true);

    public void FishOut() => animator.SetInteger(nameof(Fish), Fish = 1);
    public void FishAlive() => animator.SetInteger(nameof(Fish), Fish = 2);
}
