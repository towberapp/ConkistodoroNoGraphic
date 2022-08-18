﻿using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEvent gameEvent;
    [SerializeField]
    private UnityEvent response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(OnEventRaised);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(OnEventRaised);
    }
    private void OnDestroy()
    {
        gameEvent.UnregisterListener(OnEventRaised);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}