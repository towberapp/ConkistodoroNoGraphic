using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PressChecker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private UnityEvent _OnPress = null,
                        _OnUp = null;

    private TouchController _touchController;

    private bool _isClicked = false;

    private void Start()
    {
        _touchController = FindObjectOfType<TouchController>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (_touchController.enabled)
        {
            _OnPress.Invoke();
            _isClicked = true;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (_touchController.enabled)
        {
            _isClicked = false;
            _OnUp.Invoke();
        }
    }
}
