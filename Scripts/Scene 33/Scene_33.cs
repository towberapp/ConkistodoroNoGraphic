using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_33 : AnimatorObject
{
    private bool IsFloor1 = false;
    private bool IsFloor2 = false;
    private int BoxState = 0;
    private int CarriageState = 0;

    public void FallFloor1() => animator.SetBool(nameof(IsFloor1), IsFloor1 = true);
    public void FallFloor2() => animator.SetBool(nameof(IsFloor2), IsFloor2 = true);
    public void BoxUp() => animator.SetInteger(nameof(BoxState), BoxState = 0);
    public void BoxDown() => animator.SetInteger(nameof(BoxState), BoxState = 1);
    public void BoxLeft() => animator.SetInteger(nameof(BoxState), BoxState = 2);
    public void BoxCharge() => animator.SetInteger(nameof(BoxState), BoxState = 3);
    public void BoxShoot() => animator.SetInteger(nameof(BoxState), BoxState = 4);
    public void BoxCut() => animator.SetInteger(nameof(BoxState), BoxState = 5);
    public void CarriageHarpoon() => animator.SetInteger(nameof(CarriageState), CarriageState = 1);
    public void CarriageShoot() => animator.SetInteger(nameof(CarriageState), CarriageState = 2);

    
}
