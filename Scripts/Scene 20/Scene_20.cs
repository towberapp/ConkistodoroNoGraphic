using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_20 : AnimatorObject
{
    private int CageState = 0;
    private int PortalState = 0;
    private bool IsBeastOut = false;

    public void PumpCage() => animator.SetInteger(nameof(CageState), CageState = 1);
    public void RemoveBeast() => animator.SetBool(nameof(IsBeastOut), IsBeastOut = true);
    public void PutPortal() => animator.SetInteger(nameof(PortalState), PortalState = 1);
    public void EnablePortal() => animator.SetInteger(nameof(PortalState), PortalState = 2);
}
