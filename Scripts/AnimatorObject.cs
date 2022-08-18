using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorObject : MonoBehaviour
{
    private Animator _anim = null;

    protected Animator animator
    {
        get
        {
            if (!_anim)
                _anim = GetComponent<Animator>();
            return _anim;
        }
    }
}
