using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InventoryState))]
public class InventoryChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject _OriginChild = null;

    private List<UIItem> _Childs = new List<UIItem>();

    [SerializeField]
    private TouchController _TouchController = null;

    private PlayerInventory _PlayerItems = null;
    private PlayerInventory PlayerItems
    {
        get
        {
            if (!_PlayerItems)
                _PlayerItems = FindObjectOfType<PlayerInventory>();
            return (_PlayerItems);
        }
    }

    

    [SerializeField]
    private NewItemShower _ItemShower = null;

    private InventoryState _InventoryState = null;


    private void Start()
    {
        _TouchController = FindObjectOfType<TouchController>();
        _InventoryState = GetComponent<InventoryState>();
        StartCoroutine(Checking());
    }
    public void AddAndShow(Item item)
    {
        GameObject newChild = GameObject.Instantiate(_OriginChild, transform);
        UIItem newItem;
        if (!newChild.TryGetComponent<UIItem>(out newItem))
            newItem = newChild.AddComponent<UIItem>();
        newItem.Init(item, _TouchController);
        _Childs.Add(newItem);
        if (item.IsShow)
        {
            //_InventoryState.Show();
            newChild.GetComponent<Image>().enabled = false;
            Instantiate(_ItemShower, transform.parent).Show(newItem);
        }
    }

    private void Add(Item item)
    {
        GameObject newChild = GameObject.Instantiate(_OriginChild, transform);
        UIItem newItem;
        if (!newChild.TryGetComponent<UIItem>(out newItem))
            newItem = newChild.AddComponent<UIItem>();
        newItem.Init(item, _TouchController);
        _Childs.Add(newItem);
    }
    public void Remove(UIItem child)
    {
        _Childs.Remove(child);
        Destroy(child.gameObject);
    }

    public void Remove(Item item)
    {
        UIItem child = _Childs.Find((UIItem c) => c.Item == item);
        if (child != null)
        {
            _Childs.Remove(child);
            Destroy(child.gameObject);
        }
    }

    public void Check(bool isShowChanges)
    {
        foreach (Item item in PlayerItems.Items)
            if (_Childs.Find((UIItem uiItem) => uiItem.Item == item) == null)
                if (isShowChanges)
                    AddAndShow(item);
                else
                    Add(item);
        UIItem[] arr = new UIItem[_Childs.Count];
        _Childs.CopyTo(arr);
        foreach (UIItem child in arr)
            if (!PlayerItems.CheckItem(child.Item))
                Remove(child);
    }

    IEnumerator Checking()
    {
        while (!PlayerItems)
            yield return null;
        Check(false);
        while(true)
        {
            Check(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
