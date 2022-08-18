using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreMecanism_29 : AnimatorObject
{
    private bool IsFishOut = false;
    private bool IsTube1 = false;
    private bool IsTube2 = false;
    private bool IsTube3 = false;
    private bool IsFishWorking = false;
    private int State = 0;

    public void SetTube1() => animator.SetBool(nameof(IsTube1), IsTube1 = true);
    public void SetTube2() => animator.SetBool(nameof(IsTube2), IsTube2 = true);
    public void SetTube3() => animator.SetBool(nameof(IsTube3), IsTube3 = true);
    public void GetFishOut() => animator.SetBool(nameof(IsFishOut), IsFishOut = true);
    public void StartFishWorking() => animator.SetBool(nameof(IsFishWorking), IsFishWorking = true);
    public void GetMechOut() => animator.SetInteger(nameof(State), State = 1);
    public void SetGoldCup() => animator.SetInteger(nameof(State), State = 2);
    public void SetDome() => animator.SetInteger(nameof(State), State = 3);
    public void SetTubes() => animator.SetInteger(nameof(State), State = 4);
    public void StartWorking() => animator.SetInteger(nameof(State), State = 5);

}
