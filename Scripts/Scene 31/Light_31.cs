using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_31 : AnimatorObject
{
    [SerializeField]
    private bool IsDown = false;

    private bool IsLamp = false;

    void Start()
    {
        animator.SetBool(nameof(IsDown), IsDown);
    }

    public void SwitchPosition()
    {
        IsDown = !IsDown;
        animator.SetBool(nameof(IsDown), IsDown);
    }

    public void SetPos(bool pos)
    {
        IsDown = pos;
        animator.SetBool(nameof(IsDown), IsDown);
    }
    public void SetLamp()
    {
        IsLamp = true;
        animator.SetBool(nameof(IsLamp), IsLamp);
    }
}
