using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CupPuzzle_21 : MonoBehaviour
{
    [SerializeField] private float _Strength = 1;
    [SerializeField] private CupArrow_21[] _Arrows;
    [SerializeField] private UnityEvent _DoneEvent;



    public void ClickRight()
    {
        foreach (var arrow in _Arrows)
            arrow.MoveRight(_Strength);
    }

    public void ClickLeft()
    {
        foreach (var arrow in _Arrows)
            arrow.MoveLeft(_Strength);
    }

    public void ClickCenter()
    {
        bool isCompleted = true;
        foreach (var arrow in _Arrows)
            isCompleted = isCompleted & arrow.Check();
        if (isCompleted)
            _DoneEvent.Invoke();
    }
}
