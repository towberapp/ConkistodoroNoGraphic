using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 52)]
public class GameEvent : ScriptableObject
{
    private event Action listeners;

    public void Invoke()
    {
        listeners?.Invoke();
    }

    public void RegisterListener(Action listener)
    {
        listeners += listener;
    }

    public void UnregisterListener(Action listener)
    {
        listeners -= listener;
    }
}