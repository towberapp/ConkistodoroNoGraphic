using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemComparer : MonoBehaviour
{
    [SerializeField]
    private ItemContainer _PlayerInventory = null;

    [SerializeField]
    private TouchController _TouchController = null;

    [SerializeField]
    private InventoryChecker _InventoryChecker = null;
    void Start()
    {
        _TouchController = FindObjectOfType<TouchController>();
        _PlayerInventory = FindObjectOfType<PlayerInventory>();
        _InventoryChecker = FindObjectOfType<InventoryChecker>();
    }

    private void CheckItems(List<RaycastResult> results)
    {
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<UIItem>() is UIItem uiItem)
            {
                if (TryCompare(uiItem.Item, _TouchController.DraggingItem.Item)) 
                    return;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_TouchController.DraggingItem && Input.GetMouseButtonUp(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            if (raycastResults.Count > 0)
            {
                CheckItems(raycastResults);
            }
        }
    }

    private bool TryCompare(Item item1, Item item2)
    {
        if (_PlayerInventory.CheckItem(item1) && _PlayerInventory.CheckItem(item2) && item2.Compare(item1) is var compareResult && compareResult)
        {
            _PlayerInventory.RemoveItem(item2);
            _PlayerInventory.RemoveItem(item1);
            _PlayerInventory.AddItem(compareResult);
            _InventoryChecker.Check(false);
            return true;
        }
        return false;
    }
}
