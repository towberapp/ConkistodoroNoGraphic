using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    [SerializeField]
    protected PlayerSide[] _PreferedSides = null;

    [SerializeField]
    protected List<MapPoint> _ConnectedPoints = null;

    public Dictionary<MapPoint, float> Distances = new Dictionary<MapPoint, float>();
    
    public MapPoint[] GetConnected()
    {
        return _ConnectedPoints.ToArray();
    }

    [SerializeField]
    private float targetScale = 1;

    private void Start()
    {
        foreach (MapPoint point in _ConnectedPoints)
            Distances[point] = Vector2.Distance(transform.position, point.transform.position);
    }
    public float TargetScale
    {
        get
        {
            return targetScale;
        }
    }

    public Vector3 Position
    {
        get => transform.position;
    }

    public bool CheckConnection(MapPoint point)
    {
        return _ConnectedPoints.Contains(point);
    }

    public void Add(MapPoint newPoint)
    {
        if (!_ConnectedPoints.Contains(newPoint))
            _ConnectedPoints.Add(newPoint);
    }

    public void Remove(MapPoint newPoint)
    {
        if (_ConnectedPoints.Contains(newPoint))
            _ConnectedPoints.Remove(newPoint);
    }
    public PlayerSide GetPlayerSide(Vector2 moveVector) //проверяет, какая сторона наиболее выгодна нам 
    {
        var Xcheck = from side in _PreferedSides
                      where side.MoveVector.x == moveVector.x
                      select side;
        var Ycheck = from side in _PreferedSides
                     where side.MoveVector.y == moveVector.y
                     select side;
        if (Ycheck.Count() == 0 && Xcheck.Count() == 0)
            return _PreferedSides[0];
        else if (Ycheck.Count() == 0)
            return Xcheck.First();
        else if (Xcheck.Count() == 0)
            return Ycheck.First();

        var res = from side in Xcheck
                  where Ycheck.Contains(side)
                  select side;
        if (res.Count() == 0)
            return Xcheck.First();
        return res.First(); //если ни одна не подходит, вернет первую из списка
    }
}
