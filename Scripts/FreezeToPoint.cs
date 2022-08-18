using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FreezeToPoint : MonoBehaviour
{
    [SerializeField]
    private MapPoint _Point = null;
    // Update is called once per frame
    void Update()
    {
        transform.position = _Point.transform.position;
    }
}
