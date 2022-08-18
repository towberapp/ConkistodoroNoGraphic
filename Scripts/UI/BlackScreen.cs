using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : AnimatorObject
{
    [SerializeField] private float _startWait = 0;
    private float Speed;
    private SceneManipulator _SceneManipulator = null;

    private void Start()
    {
        _SceneManipulator = SceneManipulator.Manipulator;
        StartCoroutine(FromStart());
    }

    public void Appear()
    {
        animator.Play("Appear");
    }

    private IEnumerator FromStart()
    {
        animator.SetFloat(nameof(Speed), Speed = 0);
        yield return new WaitForSeconds(_startWait);
        animator.SetFloat(nameof(Speed), Speed = 1);
    }    

    public void AllowSwitching() => _SceneManipulator.EnableSwitching();
}
