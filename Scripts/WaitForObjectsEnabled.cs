using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitForObjectsEnabled : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _TargetObjects = null;

    [SerializeField]
    private bool[] _TargetState = null;

    [SerializeField]
    private UnityEvent _Event = null;

    private void Check()
    {
        for (int i = 0; i < _TargetObjects.Length; i++)
            if (_TargetObjects[i].activeSelf != _TargetState[i])
                return;
        _Event.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        Check();   
    }
}
