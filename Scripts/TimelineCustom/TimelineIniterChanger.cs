using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineIniterChanger : MonoBehaviour
{
    [SerializeField]
    private int _Scene = 0;

    [SerializeField]
    private TimelineIniter _Initer = null;

    public void ChangeStart(TimelineStart newStart)
    {
        _Initer.ChangeStart(_Scene, newStart);
    }
}
