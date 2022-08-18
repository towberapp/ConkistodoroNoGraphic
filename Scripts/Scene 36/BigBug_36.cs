using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBug_36 : AnimatorObject
{
    private int State = 0;

    public void SetDead() => animator.SetInteger(nameof(State), State = 1);
    public void RemoveTv() => animator.SetInteger(nameof(State), State = 2);
}
