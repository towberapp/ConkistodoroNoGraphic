using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Events;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineRunner : MonoBehaviour
{
    private PlayableDirector _Director = null;

    public PlayableDirector Director
    {
        get
        {
            if (!_Director)
                _Director = GetComponent<PlayableDirector>();
            return _Director;
        }
    }

    [SerializeField]
    private TimelineAsset[] _Clips;


    [SerializeField]
    private bool _IsRandom = false;

    [SerializeField]
    private int minWait = 0, maxWait = 10;

    [SerializeField]
    private int _RunCountForEvent = -1; //  < 0 no limits

    [SerializeField]
    private UnityEvent _Event = null;

    private int _CurrentIndex = -1;

    public int RunCount = 0;

    private void Start()
    {
        StartCoroutine(Monitoring());
    }

    private float GetWaitTime()
    {
        return Random.Range(minWait, maxWait);
    }

    private void SetNext()
    {
        if (_IsRandom)
            _CurrentIndex = Random.Range(0, _Clips.Length);
        else
            _CurrentIndex = _CurrentIndex == (_Clips.Length - 1) ? 0 : (_CurrentIndex + 1);
        Director.playableAsset = _Clips[_CurrentIndex];
        Director.Play();
    }

    private IEnumerator Monitoring()
    {
        while (true)
        {
            yield return new WaitForSeconds(GetWaitTime());
            SetNext();
            yield return null;
            while (Director.state == PlayState.Playing)
                yield return null;
            Director.playableAsset = null;
            RunCount++;
            if (_RunCountForEvent > 0 && RunCount % _RunCountForEvent == 0)
                _Event.Invoke();
        }
    }
}