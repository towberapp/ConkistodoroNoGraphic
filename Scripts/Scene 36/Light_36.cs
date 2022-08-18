using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_36 : MonoBehaviour
{
    [Header("Line Drawer Settings")]
    [SerializeField] private Transform _lineDrawer;
    [SerializeField] private AnimationCurve _lineCurve;
    [SerializeField] private float _beatDuration = 1;
    [SerializeField] private GameEvent[] _frequences;
    [SerializeField] private float[] _sizes;
    [SerializeField] private int _correctMode;
    [SerializeField] private float _deltaForIncorrect;
    [Header("Animation Settings")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _varName;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float[] _animTimes;
    private Vector3 _startPos;
    private int _currentMode = -1;
    private Coroutine _beat;

    private float Progress
    {
        get => _animator.GetFloat(_varName);
        set => _animator.SetFloat(_varName, value);
    }

    private void Start()
    {
        _startPos = _lineDrawer.position;
    }

    public void SetMode(int newMode)
    {
        if (_currentMode >= 0)
            _frequences[_currentMode].UnregisterListener(DoHeartBeat);
        _currentMode = newMode;
        _frequences[_currentMode].RegisterListener(DoHeartBeat);
    }

    private void DoHeartBeat()
    {
        if (_beat != null)
        {
            StopCoroutine(_beat);
            Vector3 pos = _lineDrawer.localPosition;
            pos.y = 0;
            _lineDrawer.localPosition = pos;
        }
        _beat = StartCoroutine(Heartbeat(_sizes[_currentMode], _animTimes[_currentMode], _currentMode != _correctMode));
    }

    private IEnumerator Heartbeat(float size, float animTime, bool isDelay)
    {
        if (isDelay)
            yield return new WaitForSeconds(_deltaForIncorrect);
        float time = Time.time;
        float progress = 0;
        Vector3 startPos = _lineDrawer.position;

        while (progress <= 1)
        {
            progress = (Time.time - time) / _beatDuration;
            Progress = _animationCurve.Evaluate(progress) * animTime;
            _lineDrawer.position = startPos + Vector3.up * _lineCurve.Evaluate(progress) * size;
            yield return null;
        }
        _lineDrawer.position = startPos + Vector3.up * _lineCurve.Evaluate(1) * size;
    }

    private void OnDestroy()
    {
        _frequences[_currentMode].UnregisterListener(DoHeartBeat);
    }
}
