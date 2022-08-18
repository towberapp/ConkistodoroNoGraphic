using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController_36 : AnimatorObject
{
    private int State = 0;

    public void SetState(int newState) => animator.SetInteger(nameof(State), State = newState);
}
