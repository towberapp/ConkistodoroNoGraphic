using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : AnimatorObject
{
    private bool    IsBroken = false,
                    IsFixed = false,
                    IsOpen = false,
                    IsWasOpened = false,
                    IsWired = false,
                    IsTubed = false,
                    FirstTube = false,
                    AllDone = false;

    private bool IsCompressorWork;

    public void CompressorOff(bool status)
    {
        animator.SetBool(nameof(IsCompressorWork), status);
    }

    public void AllDoneEvent()
    {        
        animator.SetBool(nameof(AllDone), true);
    }


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
        animator.SetTrigger("SetTubeTtrigger");
    }

   /* public void FirstTubeBool()
    {
        animator.SetBool(nameof(FirstTube), true);
        //animator.SetTrigger("SetTubeTtrigger");
    }*/

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
