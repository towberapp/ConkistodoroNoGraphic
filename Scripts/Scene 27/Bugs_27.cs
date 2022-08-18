using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugs_27 : AnimatorObject
{
    [SerializeField] private int State = 0;

    public void Fear() => animator.SetInteger(nameof(State), State = 1);
    public void OneGone() => animator.SetInteger(nameof(State), State = 2);
    public void OnDoor() => animator.SetInteger(nameof(State), State = 3);
    public void DoorAlone() => animator.SetInteger(nameof(State), State = 4);
}
