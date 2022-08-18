using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class FieldRemote_27 : AnimatorObject
{
    [SerializeField] private float _WaitForRun = 10f;
    [SerializeField] private float _WaitForLocker = 3f;
    [SerializeField] private State _EnergyFieldState = null;
    [SerializeField] private TimelineAsset _playerThrowClip;
    [SerializeField] private State _lockerState;
    [SerializeField] private TimelineManager _TimelineManager = null;
    private Coroutine _waiting;
    private int ButtonPressed = 1;

    public void SetButton(int button)
    {
        animator.SetInteger(nameof(ButtonPressed), ButtonPressed = button);
        if (button == 4)
            _waiting = StartCoroutine(RunWaiting());
    }

    public void StartWaitForLocker()
    {
        if (_waiting != null)
            StopCoroutine(_waiting);
        _waiting = StartCoroutine(LockerWaiting());
    }

    public void StopWait()
    {
        if (_waiting != null)
            StopCoroutine(_waiting);
    }

    private IEnumerator RunWaiting()
    {
        float startTime = Time.time;
        while (ButtonPressed == 4 &&  Time.time - startTime < _WaitForRun || _TimelineManager.IsPlaying)
            yield return null;
        if (ButtonPressed == 4)
            _EnergyFieldState.Value = "Biggest";
    }

    private IEnumerator LockerWaiting()
    {
        float startTime = Time.time;
        while (!_TimelineManager.IsPlaying && Time.time - startTime < _WaitForLocker || _TimelineManager.IsPlaying)
            yield return null;
        if (_lockerState.Value == "Closed")
            _EnergyFieldState.Value = "Biggest";
        else
            _TimelineManager.AddToQueue(_playerThrowClip);
    }
}
