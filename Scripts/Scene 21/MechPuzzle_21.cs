using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MechPuzzle_21 : AnimatorObject
{
    [SerializeField] private ObjectProgressManager _ButtonsState;
    [SerializeField] private string _Buttons = "00000000";
    [SerializeField] private UnityEvent _FinishEvent;
    private bool _IsFinished = false;
    private int _Light = 0;


    private void Init()
    {
        IEnumerator Waiter()
        {
            yield return new WaitForSeconds(0.1f);
            _Buttons = _ButtonsState.State.Length == 8 ? _ButtonsState.State : _Buttons;
            SetButtons(_Buttons);
        }
        StartCoroutine(Waiter());
    }

    private void Start()
    {
        Init();
    }

    public void Finish()
    {
        _FinishEvent.Invoke();
        _IsFinished = true;
    }

    public void Update()
    {
        if (_Buttons == "11111111" && !_IsFinished)
            Finish();
    }

    private int Light
    {
        get => _Light;
        set
        {
            if (value >= 0)
                animator.SetInteger(nameof(Light), _Light = value);
        }
    }

    public void AddConnection()
    {
        Light++;
        animator.Play("Connected");
    }

    public void RemoveConnection()
    {
        Light--;
        animator.Play("Connected");
    }

    public void SwitchButton(int button)
    {
        SetButton(button, _Buttons[button - 1] == '1' ? "0" : "1");
        
        if (button > 1)
            SetButton(button - 1, _Buttons[button - 2] == '1' ? "0" : "1");
        else
            SetButton(8, _Buttons[7] == '1' ? "0" : "1");

        if (button < 8)
            SetButton(button + 1, _Buttons[button] == '1' ? "0" : "1");
        else
            SetButton(1, _Buttons[0] == '1' ? "0" : "1");
        SetButtons(_Buttons);
    }

    private void SetButton(int button, string state)
    {
        _Buttons = _Buttons.Remove(button - 1, 1);
        _Buttons = _Buttons.Insert(button - 1, state);
    }

    public void SetButtons(string buttonStates)
    {
        for (int i = 0; i < _Buttons.Length; i++)
            animator.SetBool((i + 1).ToString(), buttonStates[i] == '1');
        _Buttons = buttonStates;
        _ButtonsState.SetState(_Buttons);
    }
}
