using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject _Target = null;

    [SerializeField]
    private UnityEvent _Enter = null,
                        _Stay = null,
                        _Exit = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_Enter != null && collision.gameObject == _Target)
            _Enter.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_Exit != null && collision.gameObject == _Target)
            _Exit.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_Stay != null && collision.gameObject == _Target)
            _Stay.Invoke();
    }
}
