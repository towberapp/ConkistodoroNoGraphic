using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_9 : AnimatorObject
{
    private bool IsTubeOut = false,
                IsOn = false,
                Boom = false;

    [SerializeField]
    private SpriteRenderer _Bug = null;
    [SerializeField]
    private SpriteRenderer _Tube = null;

    public void SetTubeOut()
    {
        animator.SetBool(nameof(IsTubeOut), true);
    }

    public void TurnOn()
    {
        animator.SetBool(nameof(IsOn), true);
    }

    public void BoomBox()
    {
        animator.SetBool(nameof(Boom), true);
    }

    public void SetTubeToBug()
    {
        _Tube.enabled = false;
    }

    public void SetBug()
    {
        _Bug.enabled = true;
    }
}
