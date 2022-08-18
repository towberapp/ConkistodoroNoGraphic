using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueSetterToChilds <T> : MonoBehaviour 
    where T : MonoBehaviour
{
    [SerializeField]
    protected T _ParentComponent = default(T);
    
    protected T[] _ChildComponents = null;

    private void Start()
    {
        _ChildComponents = GetComponentsInChildren<T>(true);
    }

    virtual protected void UpdateValues() { }
    virtual protected bool CheckChanges() { return false; }
}
