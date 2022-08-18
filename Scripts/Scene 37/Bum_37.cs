using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bum_37 : AnimatorObject
{
    private int State = 0;

    public void Wholed() => animator.SetInteger(nameof(State), State = 1);
    public void Sucked() => animator.SetInteger(nameof(State), State = 2);
}
