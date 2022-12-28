#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MapPoint))]
public class ConnectionDrawer : MonoBehaviour
{
    [SerializeField] private bool _AutoConnection = false;
    private MapPoint _Point = null;
    void Start()
    {
        _Point = GetComponent<MapPoint>();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (MapPoint connected in _Point.GetConnected())
        {
            if (Application.isEditor  && _AutoConnection && !connected.CheckConnection(_Point))
                connected.Add(_Point);
            if (!connected)
            {
                _Point.Remove(connected);
                break;
            }

            if (_Point is ActionMapPoint && ((ActionMapPoint)_Point).GetEvent(connected) != null
                && connected.gameObject.activeInHierarchy && connected.CheckConnection(_Point))
                Debug.DrawLine(transform.position, connected.transform.position, Color.cyan, 0.1f);
            
            else if (_Point is ActionMapPoint && ((ActionMapPoint)_Point).GetEvent(connected) != null
                && connected.gameObject.activeInHierarchy)
                Debug.DrawLine(transform.position, connected.transform.position, Color.yellow, 0.1f);
            
            else if (connected.gameObject.activeInHierarchy && connected.CheckConnection(_Point))
                Debug.DrawLine(transform.position, connected.transform.position, Color.red, 0.1f);
            
            else if (connected.gameObject.activeInHierarchy)
                Debug.DrawLine(transform.position, connected.transform.position, Color.green, 0.1f);
        }
    }
}
#endif