using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemMultiComp", order = 53)]
public class ItemMultiComp : Item
{
    [SerializeField] private List<Item> _CompItems;
    [SerializeField] private List<Item> _ResultsOfCompare;

    public override Item Compare(Item item)
    {
        int index = _CompItems.FindIndex(i => i == item);
        if (index < 0 || !IsComparable)
            return null;
        return _ResultsOfCompare[index];
    }
}
