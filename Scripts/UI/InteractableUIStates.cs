using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(StatesChecker))]
public class InteractableUIStates : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UnityEvent _ClickEvent = null;

    private StatesChecker _StateChecker = null;

    [SerializeField]
    private TouchController _TouchController = null;

    [SerializeField]
    private Item _CurrentItem = null;

    public StatesChecker StateChecker
    {
        get
        {
            if (!_StateChecker)
                _StateChecker = GetComponent<StatesChecker>();
            return _StateChecker;
        }
    }

    private void Start()
    {
        _StateChecker = GetComponent<StatesChecker>();
    }
    public void OnPointerEnter(PointerEventData data) => _CurrentItem = _TouchController.DraggingItem?.Item;
    public void OnPointerExit(PointerEventData data) => _CurrentItem = null;
    public void OnPointerUp(PointerEventData data)
    {
        TimelineAsset clip;
        (_ClickEvent, clip) = _StateChecker.GetValues(_CurrentItem);
        if (_ClickEvent != null)
            _ClickEvent.Invoke();
        _CurrentItem = null;
    }


}
