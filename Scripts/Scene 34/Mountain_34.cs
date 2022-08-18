using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_34 : MonoBehaviour
{
    public void SetToX(Transform target)
    {
        Vector3 newPos = transform.position;
        newPos.x = target.position.x;
        transform.position = newPos;
    }

    public void SetToY(Transform target)
    {
        Vector3 newPos = transform.position;
        newPos.y = target.position.y;
        transform.position = newPos;
    }
}
