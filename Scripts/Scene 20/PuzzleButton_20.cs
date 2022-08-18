using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PuzzleButton_20 : AnimatorObject
{
    [SerializeField] private AudioPlay _AudioPlay;
    [SerializeField] private AudioClip _TurningOffClip;
    [SerializeField] private AudioClip _TurningOnClip;
    [SerializeField] private List<PuzzleButton_20> _NearButtons = new List<PuzzleButton_20>();
    [SerializeField] private bool _IsUp;

    public bool IsUp
    {
        get => _IsUp;
    }

    private void Start()
    {
        SetState(_IsUp);
    }

    public void SetState(bool newState)
    {
        animator.SetBool(nameof(IsUp), newState);
        _IsUp = newState;
    }

    public void SwitchState()
    {
        SetState(!_IsUp);
    }

    public void OnClick()
    {
        if (_IsUp)
            _AudioPlay.Play(_TurningOffClip);
        else
            _AudioPlay.Play(_TurningOnClip);
        SwitchState();
        foreach (var button in _NearButtons)
            button.SwitchState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PuzzleButton_20>() is var nearButton && nearButton != null && !_NearButtons.Contains(nearButton))
            _NearButtons.Add(nearButton);
    }

}
