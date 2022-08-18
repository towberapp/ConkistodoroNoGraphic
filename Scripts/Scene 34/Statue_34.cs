using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue_34 : AnimatorObject
{
    [SerializeField] private string _KnifeStates= "000";
    [SerializeField] private State _StatueState;
    private bool Sword_1 = false;
    private bool Sword_2 = false;
    private bool Sword_3 = false;

    private string KnifeStates
    {
        get => _KnifeStates;
        set
        {
            _StatueState.Value = value;
            _KnifeStates = value;
        }
    }

    public void SetSword1(bool state)
    {
        if (Sword_1 != state)
        {
            animator.SetBool(nameof(Sword_1), Sword_1 = state);
            char newVal = Sword_1 ? '1' : '0';
            KnifeStates = $"{newVal}{KnifeStates[1]}{KnifeStates[2]}";
        }
    }
    public void SetSword2(bool state)
    {
        if (Sword_2 != state)
        {
            animator.SetBool(nameof(Sword_2), Sword_2 = state);
            char newVal = Sword_2 ? '1' : '0';
            KnifeStates = $"{KnifeStates[0]}{newVal}{KnifeStates[2]}";
        }
    }
    public void SetSword3(bool state)
    {
        if (Sword_3 != state)
        {
            animator.SetBool(nameof(Sword_3), Sword_3 = state);
            char newVal = Sword_3 ? '1' : '0';
            KnifeStates = $"{KnifeStates[0]}{KnifeStates[1]}{newVal}";
        }
    }

    public void SwitchSword1() => SetSword1(!Sword_1);
    public void SwitchSword2() => SetSword2(!Sword_2);
    public void SwitchSword3() => SetSword3(!Sword_3);

    public void SetSwords(string states)
    {
        _KnifeStates = states;
        if (states[0] == '1' && !Sword_1)
            SetSword1(!Sword_1);
        if (states[1] == '1' && !Sword_2)
            SetSword2(!Sword_2);
        if (states[2] == '1' && !Sword_3)
            SetSword3(!Sword_3);
    }

}
