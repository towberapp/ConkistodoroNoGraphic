using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_14 : AnimatorObject
{
    private bool IsDoorOpened = false,
                WaterOut = false;

    private int LightState = 0;

    public void SetLightState(int value) => animator.SetInteger(nameof(LightState), LightState = value);
    public void SetDoor(bool state)
    {
        IsDoorOpened = state;
        animator.SetBool(nameof(IsDoorOpened), state);
    }

    public void SetWater(bool state)
    {
        WaterOut = state;
        animator.SetBool(nameof(WaterOut), state);
    }
}
