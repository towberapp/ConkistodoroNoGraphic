using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_32 : AnimatorObject
{
    private bool IsLight = false,
                IsBoxOpened = false,
                IsPortalOpened = false;

    public void AddLight() => animator.SetBool(nameof(IsLight), true);

    public void SetBox(bool state) => animator.SetBool(nameof(IsBoxOpened), state);
    public void SetPortal(bool state) => animator.SetBool(nameof(IsPortalOpened), state);
}
