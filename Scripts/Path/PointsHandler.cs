using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsHandler : MonoBehaviour
{
    [SerializeField]
    private List<MapPoint> _Points = new List<MapPoint>();

    private List<MapPoint> Points
    { 
        get
        {
            if (_Points.Count == 0)
                Init();
            return _Points;
        }
    }
    
    private void Init()
    {
        foreach (MapPoint point in GetComponentsInChildren<MapPoint>(true))
            if (!_Points.Contains(point))
                _Points.Add(point);
    }
    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < Points.Count; i++)
            yield return Points[i];
    }
    public MapPoint this[int index]
    {
        get
        {
            return Points[index];
        }
    }
}
