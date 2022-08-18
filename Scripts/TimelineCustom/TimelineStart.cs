using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "TimelineStart", menuName = "TimelineStart", order = 59)]
public class TimelineStart : ScriptableObject
{
    [SerializeField]
    private TimelineAsset[] _Starts = null;

    [SerializeField]
    private bool _IsLoop = false;

    public TimelineAsset Get()
    {
        return _Starts[Random.Range(0, _Starts.Length)];
    }

    public bool IsLoop
    {
        get => _IsLoop;
    }
}
