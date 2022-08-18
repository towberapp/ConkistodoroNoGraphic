using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoffin_29 : AnimatorObject
{
    private bool IsOpenA = false;
    private bool IsOpenB = false;

    public void OpenA() => animator.SetBool(nameof(IsOpenA), IsOpenA = true);
    public void OpenB() => animator.SetBool(nameof(IsOpenB), IsOpenB = true);
    public void CloseA() => animator.SetBool(nameof(IsOpenA), IsOpenA = false);
    public void CloseB() => animator.SetBool(nameof(IsOpenB), IsOpenB = false);
}
