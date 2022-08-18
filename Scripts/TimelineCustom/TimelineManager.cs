using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

class TimelineClip
{
    public TimelineAsset Asset;
    public bool IsLoop;

    public TimelineClip(TimelineAsset asset, bool isLoop = false)
    {
        Asset = asset;
        IsLoop = isLoop;
    }
    public static bool operator !(TimelineClip clip)
    {
        return clip == null;
    }
    public static bool operator false(TimelineClip clip)
    {
        return clip == null;
    }
    public static bool operator true(TimelineClip clip)
    {
        return clip != null;
    }
    public static implicit operator bool (TimelineClip clip) => clip != null;
    public static implicit operator TimelineAsset (TimelineClip clip) => clip.Asset;

}

[RequireComponent(typeof(PlayableDirector))]
[RequireComponent(typeof(TimelineIniter))]

public class TimelineManager : MonoBehaviour
{
    public bool IsPlaying
    {
        get => _Director.state == PlayState.Playing;
    }

    private TimelineIniter _Initer = null;
    private TouchController _TouchCotroller = null;
    private PlayableDirector _Director = null;
    private List<TimelineClip> _ClipQueue = new List<TimelineClip>();
    private TimelineClip _CurrentClip;

    private void Start()
    {
        _TouchCotroller = FindObjectOfType<TouchController>();
        _Initer = GetComponent<TimelineIniter>();
        _Director = GetComponent<PlayableDirector>();
        //_Director.extrapolationMode = DirectorWrapMode.Hold;
        TimelineStart startAsset = _Initer.GetStart(SceneManipulator.Manipulator.Last);
        AddToQueue(startAsset);
    }


    private void Set(TimelineAsset asset, bool isLoop = false)
    {
        _Director.playableAsset = asset;
        if (isLoop)
        {
            _Director.extrapolationMode = DirectorWrapMode.Loop;
            if (_TouchCotroller)
                _TouchCotroller.enabled = true;
        }
        else
        {
            _Director.extrapolationMode = DirectorWrapMode.None;
            if (_TouchCotroller)
                _TouchCotroller.enabled = false;
        }
        if (asset)
            _Director.Play();
    }
    
    public void RemoveFromQueue(TimelineAsset el)
    {
        if (el && _ClipQueue.Find((newClip) => newClip.Asset == el) is TimelineClip clip && clip)
        {
            if (clip.IsLoop && _ClipQueue.FindIndex((cl) => cl == clip) == 0)
                _Director.extrapolationMode = DirectorWrapMode.None;
            _ClipQueue.Remove(clip);
        }
    }

    public void AddForward(TimelineAsset newEl)
    {
        List<TimelineClip> newQueue = new List<TimelineClip>();
        TimelineClip clip = _ClipQueue.Find((newClip) => newClip.Asset == newEl);
        if (!clip)
            clip = new TimelineClip(newEl);
        _ClipQueue.Add(clip);
        _ClipQueue.ForEach((newClip) => newQueue.Add(newClip));
        _ClipQueue = newQueue;
        _CurrentClip = clip;
        Set(newEl);
    }
    public void AddToQueue(TimelineAsset newEl)
    {
        TimelineClip clip = _ClipQueue.Find((newClip) => newClip.Asset == newEl);
        if (!clip)
            clip = new TimelineClip(newEl);
        _ClipQueue.Add(clip);
        if (_ClipQueue.Count == 1)
        {
            _CurrentClip = clip;
            Set(newEl);
        }
    }

    public void AddToQueue(TimelineStart someTimelines)
    {
        TimelineAsset newEl = someTimelines.Get();
        TimelineClip clip = _ClipQueue.Find((newClip) => newClip.Asset == newEl);
        if (!clip)
            clip = new TimelineClip(newEl, someTimelines.IsLoop);
        _ClipQueue.Add(clip);
        if (_ClipQueue.Count == 1)
        {
            _CurrentClip = clip;
            Set(newEl, someTimelines.IsLoop);
        }
    }
    public void Next()
    {
        RemoveFromQueue(_CurrentClip.Asset);
        _CurrentClip = _ClipQueue.Count > 0 ? _ClipQueue[0] : null;
        Set(_CurrentClip?.Asset, _CurrentClip?.IsLoop ?? false);
    }
    private void Update()
    {
        //if (_Director.state == PlayState.Playing && _TouchCotroller.enabled == true)
          //  _TouchCotroller.enabled = false;
        if (!IsPlaying && _ClipQueue.Count > 0)
        {
            Next();
            if (_ClipQueue.Count == 0 && _TouchCotroller.enabled == false)
                _TouchCotroller.enabled = true;
        }
    }
}
