using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechCoffin_29 : AnimatorObject
{
    private int State = 0;
    private bool IsTubed = false;
    public void Open() => animator.SetInteger(nameof(State), State = 1);
    public void GetOut() => animator.SetInteger(nameof(State), State = 2);
    public void StartWork() => animator.SetInteger(nameof(State), State = 3);
    public void SetTube() => animator.SetBool(nameof(IsTubed), IsTubed = true);
}
