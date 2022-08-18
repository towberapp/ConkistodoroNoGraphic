using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KettlePreparation : WaitingEvent
{
    [SerializeField]
    private State _PlateState = null,
                    _KettleState =null;

    public void WaitForKettle()
    {
        StartWaiting(
            () => _PlateState.Value == "On" && _KettleState.Value == "With Koffee");
    }
}
