using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SafeAnimParams
{
    public bool IsOpen = false;

    public bool IsLamped = false;

    public int LampState = 0;
}
public class Safe : AnimatorObject
{

    [SerializeField]
    private string _CurrentCombination = "000000000";

    [SerializeField]
    private ObjectProgressManager _SaveObject = null;

    [SerializeField]
    private SpriteRenderer[] _Buttons = new SpriteRenderer[9];

    [SerializeField]
    private string _RightCombination = "";

    [SerializeField]
    private UnityEvent _OpenEvent = null;

    private SafeAnimParams _Params = new SafeAnimParams();

    private void Init()
    {
        IEnumerator Waiter()
        {
            yield return new WaitForSeconds(0.1f);
            _CurrentCombination = _SaveObject.State.Length == 9 ? _SaveObject.State : "000000000";
            UpdateButtons();
        }
        StartCoroutine(Waiter());
    }
    void Start()
    {
        Init();
    }

    public void ClickButton(int i)
    {
        if (i > 9 || i < 1)
            return;
        char oldVal = _CurrentCombination[i - 1];
        _CurrentCombination = _CurrentCombination.Remove(i - 1, 1);
        _CurrentCombination = _CurrentCombination.Insert(i - 1, (oldVal == '1' ? "0" : "1"));
        UpdateButtons();
        _SaveObject.SetState(_CurrentCombination);
    }

    public void AnimOpen()
    {
        _Params.IsOpen = true;
        animator.SetBool(nameof(_Params.IsOpen), _Params.IsOpen);
    }

    public void SetLamp()
    {
        _Params.IsLamped = true;
        animator.SetBool(nameof(_Params.IsLamped), _Params.IsLamped);
    }

    public void SetLampState(int state)
    {
        _Params.LampState = state;
        animator.SetInteger(nameof(_Params.LampState), state);
    }

    public void SetNextLampState()
    {
        _Params.LampState = _Params.LampState == 2 ? 0 : _Params.LampState + 1;
        animator.SetInteger(nameof(_Params.LampState), _Params.LampState);
    }

    public void TryOpen()
    {
        if (_CurrentCombination == _RightCombination)
            _OpenEvent.Invoke();
    }
    public void UpdateButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            _Buttons[i].enabled = _CurrentCombination[i] == '1';
        }
    }
}
