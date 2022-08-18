using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InnerState
{
    Empty,
    GreyOre,
    BlueOre,
    BothOres,
    Fire
}

[RequireComponent(typeof(Animator))]
public class Stove : AnimatorObject
{
    [SerializeField]
    private State _Door = null;

    [SerializeField]
    private  State _Inner = null;
    [SerializeField]
    private InnerState _InnerEnum = InnerState.Empty;


    [SerializeField]
    private SpriteRenderer Ores = null;

    [SerializeField]
    private Sprite Ore1 = null,
                    Ore2 = null;

    [SerializeField]
    private ObjectProgressManager _DoorsRemember = null,
                                _InnerRemember = null;


    public void SetDoors(bool isOpen)
    {
        _Door.Value = isOpen ? "Opened" : "Closed";
        _DoorsRemember.SetState(_Door.Value);
        animator.SetBool("IsOpen", isOpen);
    }

    public void AddOre(int index)
    {
        if (_InnerEnum == InnerState.Empty)
            _InnerEnum += index;
        else if (_InnerEnum < InnerState.BothOres)
            _InnerEnum = (int)_InnerEnum != index ? InnerState.BothOres : _InnerEnum;
        Ores.sprite = _InnerEnum == InnerState.GreyOre ? Ore1 : Ore2;
        _Inner.Value = _InnerEnum.ToString();
        _InnerRemember.SetState(_InnerEnum.ToString());
    }

    public void SetFire()
    {
        _InnerEnum = InnerState.Fire;
        _Inner.Value = _InnerEnum.ToString();
        _InnerRemember.SetState(_InnerEnum.ToString());
        animator.SetBool("IsFire", true);
    }
    public void TryFire()
    {
        if (_InnerEnum != InnerState.Fire)
            animator.Play("Fire");
        if (_InnerEnum != InnerState.BothOres)
            return;
        SetFire();
    }
}
