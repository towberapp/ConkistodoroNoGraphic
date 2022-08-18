#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PointNaming : MonoBehaviour
{
    private Dictionary<string, GameObject> _Names = new Dictionary<string, GameObject>();

    private MapPoint[] _Childs;

    private void Rename(MapPoint point)
    {
        int i = 0;
        while (_Names.ContainsKey($"Point ({i})"))
            i++;
        point.name = $"Point ({i})";
        _Names[point.name] = point.gameObject;
    }

    private void Update()
    {
        foreach (MapPoint point in GetComponentsInChildren<MapPoint>(true))
            if (!_Names.ContainsKey(point.name))
                _Names[point.name] = point.gameObject;
            else if (_Names[point.name] != point.gameObject)
            {
                if (_Names.ContainsValue(point.gameObject))
                    foreach (KeyValuePair<string, GameObject> obj in _Names)
                        if (obj.Value == point.gameObject)
                        {
                            _Names.Remove(obj.Key);
                            break;
                        }
                Rename(point);
            } 
    }
}
#endif