using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothDoor_27 : AnimatorObject
{
    private bool IsOpened = false;
    private bool IsLocked = false;

    public void SetState(bool state) => animator.SetBool(nameof(IsOpened), IsOpened = state);
    public void SetLocker() => animator.SetBool(nameof(IsLocked), IsLocked = true);
}
