using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfkChecker : MonoBehaviour
{
    private Animator _Animator = null;

    [SerializeField]
    private float _WaitTime = 60;

    private Vector2 _LastMousePos = Vector2.zero;
    void OnEnable()
    {
        _Animator = GetComponent<Animator>();
        StartCoroutine(Checker());
    }

    IEnumerator Checker()
    {
        float startCheck = Time.time;
        Vector2 newMousePos;

        while (true)
        {
            newMousePos = Input.mousePosition;
            if (newMousePos == _LastMousePos)
            {
                if (Time.time - startCheck >= _WaitTime)
                {
                    _Animator.SetBool("IsAfk", true);
                    _Animator.SetBool("IsBack", false);
                    _Animator.SetInteger("RandomAnim", Random.Range(0, 101));
                    yield return new WaitForSeconds(1f);
                    _Animator.SetBool("IsAfk", false);
                    startCheck = Time.time;
                }
            }
            else
            {
                _LastMousePos = newMousePos;
                _Animator.SetBool("IsBack", true);
                startCheck = Time.time;
            }
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
