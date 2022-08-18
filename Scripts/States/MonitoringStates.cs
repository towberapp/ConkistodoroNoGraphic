using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StatesChecker))]
public class MonitoringStates : MonoBehaviour
{
    private string _LastStates = "";

    private StatesChecker _StatesChecker = null;

    private void Start()
    {
        _StatesChecker = GetComponent<StatesChecker>();
    }
    void Update()
    {
        string newState = _StatesChecker.UpdateStates();
        if (newState != _LastStates)
        {
            _LastStates = newState;
            var (complEvent, clip) = _StatesChecker.GetValues(null);
            complEvent?.Invoke();
        }
    }
}
