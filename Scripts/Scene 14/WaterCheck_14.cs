using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCheck_14 : MonoBehaviour
{
    [SerializeField] private State _WaterState;
    [Range(0, 100)][SerializeField] private int _Percents;

    public void Check()
    {
        if (_WaterState.Value == "Down" && Random.Range(0, 100) - _Percents >= 0)
            _WaterState.Value = "Water Gone";
    }
}
