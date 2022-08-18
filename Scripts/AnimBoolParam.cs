using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBoolParam : MonoBehaviour
{
    private Animator _Anim = null;
    [SerializeField]
    private string _Name = "";
    [SerializeField]
    private bool _Value = false;
    private Animator Anim
    {
        get
        {
            if (!_Anim)
            { 
                _Anim = GetComponentInParent<Animator>();
                _Anim.SetBool(_Name, _Value);
            }
            return _Anim;
        }
    }

    public void SwitchBool()
    {
        _Value = !_Value;
        Anim.SetBool(_Name, _Value);
    }
    public void SetValue(bool newVal)
    {
        _Value = newVal;
        Anim.SetBool(_Name, _Value);
    }
}
