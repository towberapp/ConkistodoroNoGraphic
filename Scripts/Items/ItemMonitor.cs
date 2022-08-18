using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemMonitor : MonoBehaviour
{
    [SerializeField] private List<Item> _Targets;
    [SerializeField] private List<UnityEvent> _Events;

    private PlayerInventory _playerInventory;
    [SerializeField] private List<Item> _lastCheck = new List<Item>();

    private void Start()
    {
        _playerInventory = FindObjectOfType<PlayerInventory>();
        StartCoroutine(Monitoring());
    }

    private List<Item> GetNewItems()
    {
        return _playerInventory.Items.FindAll(item => !_lastCheck.Contains(item));
    }

    private IEnumerator Monitoring()
    {
        List<Item> newItems;

        yield return null;
        yield return null;
        _lastCheck = GetNewItems();
        while (true)
        {
            newItems = GetNewItems();
            for (int i = 0; i < _Targets.Count; i++)
            {
                if (newItems.Contains(_Targets[i]))
                    _Events[i].Invoke();
            }
            if (newItems.Count != 0)
                _lastCheck.AddRange(newItems);
            yield return null;
        }
    }
}
