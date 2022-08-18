using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_36 : AnimatorObject
{
    private int Mex = 0;
    private bool IsLockerReady = false;
    private bool IsAkumMoved = false;

    public void StablelizeMex() => animator.SetInteger(nameof(Mex), Mex = 1);
    public void FixMex() => animator.SetInteger(nameof(Mex), Mex = 2);
    public void MoveLocker() => animator.SetBool(nameof(IsLockerReady), IsLockerReady = true);
    public void MoveAkum() => animator.SetBool(nameof(IsAkumMoved), IsAkumMoved = true);
}
