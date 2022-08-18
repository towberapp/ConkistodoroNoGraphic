using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorIniter : MonoBehaviour
{
    [SerializeField]
    private float _InitSpeed = 100;

    [SerializeField]
    private int _FramesForInit = 10;

    private Animator _anim = null;

    private Animator _Animator
    {
        get
        {
            if (!_anim)
                _anim = GetComponent<Animator>();
            return _anim;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        _Animator.speed = _InitSpeed;
        for (int i = 0; i < _FramesForInit; i++)
            yield return null;
        _Animator.speed = 1;
    }
}
