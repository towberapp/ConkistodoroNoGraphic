using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardChecker : MonoBehaviour
{
    [SerializeField] private List<KeyCode> _KeysCheck = new List<KeyCode>();
    [SerializeField] private List<UnityEvent> _PressedEvents = new List<UnityEvent>();

    void Update()
    {
        for (int i = 0; i < _KeysCheck.Count; i++)
            if (Input.GetKeyDown(_KeysCheck[i]))
                _PressedEvents[i].Invoke();
    }
}
