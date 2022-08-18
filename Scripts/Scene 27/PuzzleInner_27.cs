using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInner_27 : AnimatorObject
{
    [SerializeField] private bool IsLocked = true;
    [SerializeField] private Item _NoBug = null, _HalfBug = null, _FullBug = null;
    private int InnerState = 0;
    private Item _puttedItem = null;
    
    public Item PuttedItem
    {
        get => _puttedItem;
    }

    public void Lock() => animator.SetBool(nameof(IsLocked), IsLocked = true);
    public void Unlock() => animator.SetBool(nameof(IsLocked), IsLocked = false);

    private void Change(Item newItem, int animState)
    {
        _puttedItem = newItem;
        animator.SetInteger(nameof(InnerState), InnerState = animState);
    }

    private void Empty()
    {
        Change(null, 0);
    }
    private void NoBug()
    {
        Change(_NoBug, 1);
    }
    private void HalfBug()
    {
        Change(_HalfBug, 2);
    }
    private void FullBug()
    {
        Change(_FullBug, 3);
    }

    public void SetItem(Item newItem)
    {
        if (newItem == _NoBug)
            NoBug();
        else if (newItem == _HalfBug)
           HalfBug();
        else if (newItem == _FullBug)
           FullBug();
        else
            Empty();
    }
}
