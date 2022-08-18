using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Batiskaf_19 : AnimatorObject
{
    public Rope_19 CurrentRope;

    [SerializeField] private BatiskafController_19 _Remote;
    [SerializeField] private TimelineManager _TimelineManager;
    [SerializeField] private State _BatiskafState;
    [SerializeField] private State _RopeState;

    private Point_19 _CurrentPoint;
    private TimelineAsset _RopeTimeline;
    private int _CurrentRopeId;
    
    private int CurrentRopeId
    {
        get => _CurrentRopeId;
        set
        {
            if (value > 2 || value < 0)
                _CurrentRopeId = 0;
            _CurrentRopeId = value;
        }
    }

    private void Start()
    {
        CurrentRope.IsAttractive = true;
    }

    public void Go()
    {
        _TimelineManager.AddToQueue(_RopeTimeline);
        _Remote.Disappear();
    }

    public void SetToPoint(Point_19 point)
    {
        _CurrentPoint = point;
        animator.Play(point.Name);
    }

    public void SetRope(int ropeId)
    {
        CurrentRope.IsAttractive = false;
        CurrentRopeId = ropeId;
        if (!_CurrentPoint)
            return;
        (CurrentRope, _RopeTimeline) = _CurrentPoint.GetRopeInfo(CurrentRopeId);
        _CurrentPoint.AttractToRope(CurrentRopeId);
    }

    public void ArriveTo(Point_19 point)
    {
        var ropeId = point.GetRopeId(CurrentRope);

        SetToPoint(point);
        _BatiskafState.Value = point.Name;

        SetRope(ropeId);
        _RopeState.Value = CurrentRopeId.ToString();

        _Remote.Appear();
    }
}
