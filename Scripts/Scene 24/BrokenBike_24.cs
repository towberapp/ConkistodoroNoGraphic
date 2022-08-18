using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBike_24 : AnimatorObject
{
    private int State = 0;

    public void OilOut() => animator.SetInteger(nameof(State), State = 1);
    public void HeatIce() => animator.SetInteger(nameof(State), State = 2);
    public void OilIn() => animator.SetInteger(nameof(State), State = 3);
    public void TakeLeg() => animator.SetInteger(nameof(State), State = 4);
}
