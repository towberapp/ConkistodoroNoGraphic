using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableState : State
{
    [SerializeField]
    private List<string> _AllStates = null;
    [SerializeField]
    private int _Index = 0;
    private void Start()
    {
        //if (_AllStates.Count > _Index)
          //  _Value = _AllStates[_Index];
    }

    public void NextState()
    {
        _Index = _Index == _AllStates.Count - 1 ? 0 : ++_Index;
        _Value = _AllStates[_Index];
    }
    public void SetState(string newState)
    {
        int newIndex = _AllStates.FindIndex(state => state == newState);
        if (newIndex == -1)
        {
            Debug.Log($"{gameObject.name} has not state {newState}");
            return;
        }
        _Index = newIndex;
        _Value = _AllStates[_Index];
    }
}
