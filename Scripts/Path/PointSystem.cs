using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField]
    private List <MapPoint> _AllPoints = null;

    [SerializeField]
    private List<PointsHandler> _PointsHandlers = new List<PointsHandler>();

    private void Start()
    {
        foreach (PointsHandler extraHandler in _PointsHandlers)
            AddPoints(extraHandler);
    }


    public MapPoint GetPointByName(string name)
    {
        foreach (MapPoint point in GetComponentsInChildren<MapPoint>(true))
            if (point.name == name)
                return point;
        return null;
    }
    public void AddExtra(PointsHandler extraHandler)
    {
        if (_PointsHandlers.Contains(extraHandler))
            return;
        _PointsHandlers.Add(extraHandler);
        AddPoints(extraHandler);
    }
    public void AddPoints(PointsHandler extraPoints)
    {
        extraPoints.gameObject.SetActive(true);
        foreach (MapPoint newPoint in extraPoints)
            if (!_AllPoints.Contains(newPoint))
                _AllPoints.Add(newPoint);
    }
    public void RemovePoints(PointsHandler extraPoints)
    {
        if (!_PointsHandlers.Contains(extraPoints))
            return;
        extraPoints.gameObject.SetActive(false);
        _PointsHandlers.Remove(extraPoints);
        foreach (MapPoint newPoint in extraPoints)
            if (_AllPoints.Contains(newPoint))
                _AllPoints.Remove(newPoint);
    }

    public MapPoint GetNearestPoint(Vector2 position)
    {
        float minDistance = float.MaxValue;
        MapPoint currentPoint = _AllPoints[0];
        foreach (MapPoint point in _AllPoints)
        {
            if (point?.gameObject?.activeSelf ?? false)
            {
                float distance = Vector2.Distance(point.transform.position, position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    currentPoint = point;
                }
            }
        }
        return currentPoint;
    }
    private void CheckPrevPoint(Path path, MapPoint prevPoint)
    {
        if (path != null)
            if (!(path.GetNextPoint() == prevPoint))
                path.ResetPath();
    }
    public Path GetPath(Vector3 position, MapPoint prevPoint, MapPoint finishPoint)
    {
        MapPoint startPoint = GetNearestPoint(position);
        Path path = GetPath(startPoint, finishPoint);
        CheckPrevPoint(path, prevPoint);
        return path;
    }
    public Path GetPath(Vector3 position, MapPoint finishPoint)
    {
        MapPoint startPoint = GetNearestPoint(position);
        Path path = GetPath(startPoint, finishPoint);
        return path;
    }

    public Path GetPath(MapPoint startPoint, MapPoint prevPoint, MapPoint finishPoint)
    {
        Path path = GetPath(startPoint, finishPoint);
        CheckPrevPoint(path, prevPoint);
        return path;
    }
    public Path GetPath(MapPoint startPoint, MapPoint prevPoint, Vector2 finishTarget)
    {
        Path path = GetPath(startPoint, finishTarget);
        CheckPrevPoint(path, prevPoint);
        return path;
    }
    public Path GetPath(MapPoint startPoint, Vector2 targetPos)
    {
        MapPoint finishPoint = GetNearestPoint(targetPos);
        return GetPath(startPoint, finishPoint);
    } 
    public Path GetPath(MapPoint startPoint, MapPoint finishPoint)
    {
        return CalcPath(startPoint, finishPoint);
    }

    private struct MapPathData
    {
        internal float distance;
        internal MapPoint prevPoint;
    }

    public void AddPoint(MapPoint newPoint)
    {
        if (!_AllPoints.Contains(newPoint))
        {
            _AllPoints.Add(newPoint);
        }
    }

    public void RemovePoint(MapPoint point)
    {
        if (_AllPoints.Contains(point))
            _AllPoints.Remove(point);
    }
    private MapPoint FindNotVisited(Dictionary<MapPoint, MapPathData> pointsDist, List<MapPoint> visited)
    {
        float minDistance = float.MaxValue;
        MapPoint res = null;
        foreach (MapPoint point in pointsDist.Keys)
        {
            if (!visited.Contains(point) && pointsDist[point].distance < minDistance)
            {
                minDistance = pointsDist[point].distance;
                res = point;
            }
        }
        return res;
    }
    private Dictionary<MapPoint, MapPathData> InitDictionary(out Dictionary<MapPoint, MapPathData> dict, MapPoint startPoint)
    {
        dict = new Dictionary<MapPoint, MapPathData>();
        foreach (MapPoint point in _AllPoints)
        {
            MapPathData data = new MapPathData() { distance = float.MaxValue, prevPoint = null };
            if (point == startPoint)
                data.distance = 0;
            dict[point] = data;
        }
        return dict;
    }

    private List<MapPoint> GetVisitedPoints(Dictionary<MapPoint, MapPathData> dict)
    {
        List<MapPoint> result = new List<MapPoint>();
        foreach (var point in dict.Keys)
        {
            MapPathData value;
            dict.TryGetValue(point, out value);
            if (value.distance != float.MaxValue)
                result.Add(point);
        }
        return result;
    }
    private Path CalcPath(MapPoint startPoint, MapPoint finishPoint)
    {
        Path newPath = new Path();
        List<MapPoint> visitedPoints = new List<MapPoint>();
        Dictionary<MapPoint, MapPathData> pointsDist;
        InitDictionary(out pointsDist, startPoint);
        MapPoint currentPoint = startPoint;
        while (currentPoint && currentPoint != finishPoint)
        {
            float currDist = pointsDist[currentPoint].distance;
            foreach (MapPoint connected in currentPoint.GetConnected())
            {
                if (connected.gameObject.activeInHierarchy && _AllPoints.Contains(connected) && !visitedPoints.Contains(connected))
                {
                    float distanceToConnected = currDist + currentPoint.Distances[connected];
                    if (distanceToConnected < pointsDist[connected].distance)
                        pointsDist[connected] = new MapPathData() { distance = currDist + currentPoint.Distances[connected], prevPoint = currentPoint };
                }
            }
            visitedPoints.Add(currentPoint);
            currentPoint = FindNotVisited(pointsDist, visitedPoints);
        }

        if (currentPoint == finishPoint)
        {
            if (pointsDist[finishPoint].prevPoint == null)
            {
                newPath.AddPointStart(currentPoint);
                return newPath;
            }
        }
        else
        {
            MapPoint newFinish = null;
            float minDistance = float.MaxValue;
            foreach (MapPoint point in GetVisitedPoints(pointsDist))
            {
                float distance = Vector2.Distance(finishPoint.transform.position, point.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    newFinish = point;
                }    
            }
            currentPoint = newFinish;
        }    
        while (currentPoint)
        {
            newPath.AddPointStart(currentPoint);
            currentPoint = pointsDist[currentPoint].prevPoint;
        }
        return newPath;
    }
}
