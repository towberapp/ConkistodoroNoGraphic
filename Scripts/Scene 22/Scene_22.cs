using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_22 : AnimatorObject
{
    [SerializeField] private bool IsDownDoorOpen = false;

    public void OpenDoor() => animator.SetBool(nameof(IsDownDoorOpen), IsDownDoorOpen = true);
}
