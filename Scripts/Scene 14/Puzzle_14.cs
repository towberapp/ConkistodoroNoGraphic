using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_14 : AnimatorObject
{
    private bool IsDoorOpened = false;
    private int Thermometer = 0;

    public void SetDoor(bool state)
    {
        IsDoorOpened = state;
        animator.SetBool(nameof(IsDoorOpened), state);
    }

    public void SetThermometer(int state)
    {
        Thermometer = state;
        animator.SetInteger(nameof(Thermometer), state);
    }
}
