using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path 
{
    protected List<MapPoint> _Points = null;
    private int _Index = 0;

    public MapPoint CurrentPoint
    {
        get
        {
            return _Points[_Index];
        }
    }

    public MapPoint LastPoint
    {
        get
        {
            return _Points[_Points.Count - 1];
        }
    }
    public Path(List<MapPoint> points)
    {
        _Points = points;
    }
    public Path() : this(new List<MapPoint>()) { }
    public Path(Path path)
    {
        _Points = new List<MapPoint>(path._Points);
    }

    public void ClearPath()
    {
        _Index = 0;
        _Points.Clear();
    }

    public bool ContainsPoint(MapPoint point)
    {
        return _Points.Contains(point);
    }
    public void ResetPath()
    {
        _Index = 0;
    }

    public void AddPointEnd(MapPoint point)
    {
        _Points.Add(point);
    }
    public void AddPointStart(MapPoint point)
    {
        _Points.Insert(0, point);
    }
    public MapPoint GetNextPoint()
    {
        if (_Index >= _Points.Count - 1)
            return null;
        return _Points[++_Index];
    }
    public float CalculateDistance()
    {
        float distance = 0;
        if (_Points.Count <= 1)
            return 0;
        for (int i = 1; i < _Points.Count; i++)
        {
            Vector2 firstPointPos = _Points[i - 1].transform.position;
            Vector2 secondPointPos = _Points[i].transform.position;
            distance += Vector2.Distance(firstPointPos, secondPointPos);
        }
        return distance;
    }
}
