using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Events;

[System.Serializable]
[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    //[SerializeField]
    //private ItemContainer _CheckTarget = null;

    [SerializeField]
    private Item _NecesseryItem = null;

    [SerializeField] private bool _isPointed = true;

    public bool IsPointed
    {
        get => _isPointed;
    }

    [SerializeField]
    private MapPoint _Point = null;

    public MapPoint Point
    {
        get
        {
            return _Point;
        }
    }

    [SerializeField]
    protected TimelineAsset _TimelineAsset = null;

    [SerializeField]
    private TimelineManager _TimelineManager = null;

    [SerializeField]
    protected UnityEvent _InteractEvent = null;

    public virtual bool Check(Item item)
    {
        return item == _NecesseryItem;
    }

    public virtual void Interact()
    {
        _InteractEvent.Invoke();
        _TimelineManager.AddToQueue(_TimelineAsset);
    }
}
