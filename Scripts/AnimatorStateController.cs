using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorStateController : MonoBehaviour
{
    private int _State = 0;

    [SerializeField]
    private int _PrevState = 1;


    public int State
    {
        get
        {
            return _State;
        }
        set
        {
            _PrevState = _State;
            _State = value;
        }
    }

    private Animator _Animator;
    private Animator Animator
    {
        get
        {
            if (!_Animator)
                _Animator = GetComponent<Animator>();
            return _Animator;
        }
    }

    public void SetPrevious()
    {
        State = _PrevState;
        Animator.SetInteger("State", State);
    }
    public void SetActiveState(int state)
    {
        if (_State == 0)
        {
            _PrevState = state;
        }
        else
        {
            _State = state;
            Animator.SetInteger("State", State);
        }    
    }

    public void NextState()
    {
        Animator.SetInteger("State", ++State);
    }
    public void SetState(int newState)
    {
        State = newState;
        Animator.SetInteger("State", State);
    }
}
