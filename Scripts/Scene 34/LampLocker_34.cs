using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLocker_34 : AnimatorObject
{
    private bool IsOpened = false;
    private bool IsLamped = false;

    public void OpenDoor() => animator.SetBool(nameof(IsOpened), IsOpened = true);
    public void CloseDoor() => animator.SetBool(nameof(IsOpened), IsOpened = false);
    public void SetLamp() => animator.SetBool(nameof(IsLamped), IsLamped= true);
}
