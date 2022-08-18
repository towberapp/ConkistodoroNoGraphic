using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle_31 : AnimatorObject
{
    [SerializeField] private List<PuzzleButton_31> _Buttons = new List<PuzzleButton_31>();
    [SerializeField] private UnityEvent _SolveEvent = null;
    private bool IsCharged = false;
    private bool IsAppeared = false;
    private StatesChecker _StatesChecker = null;

    private void Start()
    {
        _StatesChecker = GetComponent<StatesChecker>();
        StartCoroutine(CheckSolving());
    }

    public void Charge() => animator.SetBool(nameof(IsCharged), IsCharged = true);
    public void Appear() => animator.SetBool(nameof(IsAppeared), IsAppeared = true);
    public void Disappear() => animator.SetBool(nameof(IsAppeared), IsAppeared = false);

    public void ClickButton(string buttonName)
    {
        PuzzleButton_31 button = _Buttons.Find((el) => el.Name == buttonName);
        Debug.Log(button.Name);
        if (button && button.IsRefreshed)
        {
            button.SetClicked();
            animator.Play($"{button.Name} Click");
        }
    }

    public void SetReadyButton(string buttonName)
    {
        _Buttons.Find((el) => el.Name == buttonName).SetReady(true);
    }

    public void UnSetReadyButton(string buttonName)
    {
        _Buttons.Find((el) => el.Name == buttonName).SetReady(false);
    }

    public void StartWainitgButton(string buttonName)
    {
        _Buttons.Find((el) => el.Name == buttonName).SetWaiting();
    }

    public void RefreshButton(string buttonName)
    {
        PuzzleButton_31 button = _Buttons.Find((el) => el.Name == buttonName);
        if (button && button.IsClicked)
        {
            button.Refresh();
            animator.Play($"{button.Name} Refresh");
        }
    }

    public void RefreshButton(PuzzleButton_31 button)
    {
        if (button && button.IsClicked)
        {
            button.Refresh();
            animator.Play($"{button.Name} Refresh");
        }
    }

    public void RefreshButtons()
    {
        foreach (var button in _Buttons)
            RefreshButton(button);
        StopAllCoroutines();
        StartCoroutine(CheckSolving());
    }

    private IEnumerator CheckSolving()
    {
        while (_Buttons.FindAll((el) => el.IsReady).Count != _Buttons.Count)
        {
            if (_Buttons.FindAll((el) => el.IsWaiting).Count == _Buttons.Count)
                RefreshButtons();
            yield return null;
        }
        (var finishEvent, var _) = _StatesChecker.GetValues(null);
        finishEvent?.Invoke();
    }
}
