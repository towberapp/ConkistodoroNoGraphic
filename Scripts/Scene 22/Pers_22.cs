using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pers_22 : AnimatorObject
{
    private int State = 0;

    public void RunRight() => animator.SetInteger(nameof(State), State = 1);
    public void RunAway() => animator.SetInteger(nameof(State), State = 2);
}
