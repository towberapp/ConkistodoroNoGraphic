using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp_37 : AnimatorObject
{
    private int State = 0;

    public void SetCrashed() => animator.SetInteger(nameof(State), State = 1);
    public void SetUsed() => animator.SetInteger(nameof(State), State = 2);
}
