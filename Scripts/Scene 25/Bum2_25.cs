using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bum2_25 : AnimatorObject
{
    private int ExtraAnim = 0;

    public void UpdateState()
    {
        ExtraAnim = Random.Range(0, 2);
        animator.SetInteger(nameof(ExtraAnim), ExtraAnim);
    }
}
