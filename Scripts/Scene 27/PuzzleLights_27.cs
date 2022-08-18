using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLights_27 : AnimatorObject
{
    private int ActiveLamp = 0;

    public void SetLamp(int newLamp) => animator.SetInteger(nameof(ActiveLamp), ActiveLamp = newLamp);
}
