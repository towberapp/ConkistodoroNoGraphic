using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StatesChecker))]
public class InteractableObjectStates : InteractableObject
{
    protected StatesChecker _Checker = null;

    private void Start()
    {
        _Checker = GetComponent<StatesChecker>();
    }
    public override bool Check(Item item)
    {
        (_InteractEvent, _TimelineAsset) = _Checker.GetValues(item);
        return _InteractEvent != null && _TimelineAsset;
    }
}
