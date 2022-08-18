using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kual_37 : AnimatorObject
{
    [SerializeField] private State _DoorButtonState;
    [SerializeField] private float _WaitTime = 5;
    private string SetButton;
    private Coroutine _switching;

    public void SwitchButton() => _DoorButtonState.Value = "Down";

    private IEnumerator Switching()
    {
        yield return new WaitForSeconds(_WaitTime);
        if (_DoorButtonState.Value == "Up")
            animator.Play(nameof(SetButton));
    }


    void Update()
    {
        if (_DoorButtonState.Value == "Up" && _switching == null)
            _switching = StartCoroutine(Switching());
    }
}
