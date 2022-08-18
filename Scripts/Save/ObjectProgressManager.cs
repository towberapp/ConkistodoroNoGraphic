using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectProgressManager : MonoBehaviour
{
    [SerializeField]
    private SceneProgressManager _SceneProgressManager = null;

    [SerializeField]
    private string _ObjectName = "";

    [SerializeField]
    private string _CurrentState = "";

    [SerializeField]
    private List<string> _States = new List<string>();

    private bool _IsInited = false;
    public string State
    {
        get
        {
            if (!_IsInited)
                Init();
            return _CurrentState;
        }
    }
    [SerializeField]
    private List<UnityEvent> _Events = new List<UnityEvent>();
    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        string objectState = _SceneProgressManager.GetObjectState(_ObjectName);
        int state = _States.FindIndex((string checkState) => checkState == objectState);
        if (state != -1)
            _Events[state].Invoke();
        _CurrentState = objectState;
        _IsInited = true;
    }
    public void SetState(string state)
    {
        _CurrentState = state;
        _SceneProgressManager.ChangeState(_ObjectName, state);
    }
}
