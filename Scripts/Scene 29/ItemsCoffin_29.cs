using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCoffin_29 : AnimatorObject
{
    private int State = 0;
    private bool IsDoorOpened = false;

    public void OpenDoor() => animator.SetBool(nameof(IsDoorOpened), IsDoorOpened = true);
    public void SetDoubleDoor() => animator.SetInteger(nameof(State), State = 1);
}
