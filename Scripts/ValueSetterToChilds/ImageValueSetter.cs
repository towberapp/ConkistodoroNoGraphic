using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ImageValueSetter : ValueSetterToChilds<Image>
{
    private Color _LastColor = Color.clear;
    private bool _LastRaycastState = false;
    protected override void UpdateValues()
    {
        _LastColor = _ParentComponent.color;
        _LastRaycastState = _ParentComponent.raycastTarget;
        foreach (Image child in _ChildComponents)
        {
            child.color = _ParentComponent.color;
            child.raycastTarget = _ParentComponent.raycastTarget;
        }
    }

    protected override bool CheckChanges()
    {
        return !(_ParentComponent.color == _LastColor && _ParentComponent.raycastTarget == _LastRaycastState);
    }
    private void Update()
    {
        if (CheckChanges())
            UpdateValues();
    }
}
