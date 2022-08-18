using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : AnimatorObject
{
    private bool IsBroken = false,
                    IsFixed = false,
                    IsOpen = false,
                    IsWasOpened = false,
                    IsWired = false,
                    IsTubed = false;


    public void Brake()
    {
        animator.SetBool(nameof(IsBroken), true);
    }

    public void Fix()
    {
        animator.SetBool(nameof(IsFixed), true);
    }

    public void SetTube()
    {
        animator.SetBool(nameof(IsTubed), true);
    }

    public void SetWire()
    {
        animator.SetBool(nameof(IsWired), true);
    }

    public void SetWasOpened()
    {
        IEnumerator Setting()
        {
            yield return null;
            animator.SetBool(nameof(IsWasOpened), true);
        }
        StartCoroutine(Setting());
    }
    public void OpenDoor()
    {
        animator.SetBool(nameof(IsOpen), true);
    }

    public void CloseDoor()
    {
        animator.SetBool(nameof(IsOpen), false);
    }
}
