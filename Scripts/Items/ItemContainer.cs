using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemContainer : MonoBehaviour
{
    [SerializeField]
    protected List<Item> _Items = null;
    virtual public List<Item> Items
    {
        get
        {
            if (_Items == null)
                _Items = new List<Item>();
            return _Items;
        }
        protected set => _Items = value;
    }

    [SerializeField]
    protected UnityEvent onAdding = null,
        onRemove = null;

    public int Count
    {
        get
        {
            return Items.Count;
        }
    }
    public Item this[int index]
    {
        get
        {
            return Items[index];
        }
    }

    public bool CheckItem(Item item)
    {
        return Items.Contains(item);
    }
    public Item[] GetItems()
    {
        Item[] itemsCpy = new Item[Items.Count];
        Items.CopyTo(itemsCpy);
        return itemsCpy;
    }
    public void AddItemFromAssets(string itemName)
    {
        Item newItem = Resources.Load<Item>($"Items/{itemName}");
        if (!newItem)
        {
            Debug.Log($"Loading: {itemName} not found!");
            return;
        }
        _Items.Add(newItem);
        Debug.Log($"Loading: {itemName} loaded!");
    }

    public void AddItemsFromAssets(string[] itemNames)
    {
        foreach (string itemName in itemNames)
            AddItemFromAssets(itemName);
    }
    public void AddItem(Item item)
    {
        if (Items.Contains(item))
            return;
        Items.Add(item);
        onAdding.Invoke();
    }
    public void AddItems(List<Item> items)
    {
        foreach (Item item in items)
            AddItem(item);
        if (items != null && items.Count != 0)
            onAdding.Invoke();
    }
    public void AddItems(Item[] items)
    {
        foreach (Item item in items)
            AddItem(item);
        if (items != null && items.Length != 0)
            onAdding.Invoke();
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
        onRemove.Invoke();
    }
    public bool CheckItems(List<Item> items)
    {
        foreach (Item item in items)
        {
            if (!CheckItem(item))
                return false;
        }
        return true;
    }
}
