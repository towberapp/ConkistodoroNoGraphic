using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionMapPoint : MapPoint
{
    [SerializeField]
    private List<UnityEvent> _ConnectedEvents = null;

    public UnityEvent GetEvent(MapPoint point)
    {
        int index = _ConnectedPoints.IndexOf(point);
        return index < 0 || index >= _ConnectedEvents.Count ? null : _ConnectedEvents[index] ;
    }
}
