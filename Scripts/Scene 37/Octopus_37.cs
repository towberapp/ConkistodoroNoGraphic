using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus_37 : AnimatorObject
{
    [SerializeField] private Color _LightOn;
    [SerializeField] private Color _LightOff;

    private bool IsAppear = false;
    private ColorFilter _colorFilter;

    private ColorFilter colorFilter
    {
        get
        {
            if (_colorFilter == null)
                _colorFilter = GetComponent<ColorFilter>();
            return _colorFilter;
        }
    }

    public void LightOff() => colorFilter.FilterColor = _LightOff;
    public void LightOn() => colorFilter.FilterColor = _LightOn;
    public void Appear() => animator.SetBool(nameof(IsAppear), IsAppear = true);
}
