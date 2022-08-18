using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class TimelineIniter : MonoBehaviour
{
    [SerializeField]
    public TimelineStart _DefaultClip = null;
    
    [SerializeField]
    private List<int> _Scenes = new List<int>();

    [SerializeField]
    private List<TimelineStart> _Starts = new List<TimelineStart>();

    public TimelineStart GetStart(int prevScene)
    {
        int i = _Scenes.FindIndex(0, (int a) => a == prevScene);
        if (i == -1)
            return _DefaultClip;
        return _Starts[i];
    }

    public void ChangeStart(int scene, TimelineStart newStart)
    {
        int i = _Scenes.FindIndex(0, (a) => a == scene);
        if (i != -1)
            _Starts[i] = newStart;
    }
}
