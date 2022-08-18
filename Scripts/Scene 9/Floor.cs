using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Floor : MonoBehaviour
{
    [SerializeField]
    private State _FloorState = null;

    [SerializeField]
    private ObjectProgressManager _FloorSaver = null;

    public void SwitchState()
    {
        _FloorState.Value = _FloorState.Value == "Off" ? "On" : "Off";
        _FloorSaver.SetState(_FloorState.Value);
    }
}
