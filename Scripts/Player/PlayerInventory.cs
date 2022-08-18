using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : ItemContainer
{
    [SerializeField]
    private bool _IsInited = false; 

    private GameProgressManager _ProgressManager = null;
    private GameProgressManager ProgressManager
    {
        get
        {
            if (!_ProgressManager)
                _ProgressManager = GameProgressManager.Manager;
            return _ProgressManager;
        }
    }
       
    new public List<Item> Items
    {
        get
        {
            if (!_IsInited)
            {
                _Items = new List<Item>();
                string[] itemNames = ProgressManager.PlayerData.Inventory;
                foreach (string item in itemNames)
                    Debug.Log(item);
                AddItemsFromAssets(itemNames);
                _IsInited = true;
            }
            return _Items;
        }
        protected set => _Items = value;
    }

    public void UpdateInventory()
    {
        string[] newItems = new string[Items.Count];
        for (int i = 0; i < Items.Count; i++)
        { 
            newItems[i] = Items[i].name;
        }
        ProgressManager.PlayerData.Inventory = newItems;
    }
}
