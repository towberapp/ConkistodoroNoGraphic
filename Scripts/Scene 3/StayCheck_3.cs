using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StayCheck_3 : MonoBehaviour
{
    [SerializeField] private float _AfkTime;
    [SerializeField] private UnityEvent _Event;
    private float _LastTime;
    private bool _IsCheck;

    private void Update()
    {
        if (_IsCheck && Time.time - _LastTime >= _AfkTime)
        { 
            _Event.Invoke();
            _LastTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterMovement>() is var player && player != null)
        {
            _LastTime = Time.time;
            _IsCheck = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterMovement>() is var player && player != null)
            _IsCheck = false;
    }
}
