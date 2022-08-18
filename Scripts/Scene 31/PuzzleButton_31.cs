using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton_31 : MonoBehaviour
{
    [SerializeField]
    private string _Name = "";
    [SerializeField]
    private bool _IsClicked = false,
                _IsRefreshed = true,
                _IsReady = false,
                _IsWaiting = false;

    public string Name
    {
        get => _Name;
    }

    public bool IsClicked
    {
        get => _IsClicked;
    }

    public bool IsRefreshed
    {
        get => _IsRefreshed;
    }

    public bool IsReady
    {
        get => _IsReady;
    }

    public bool IsWaiting
    {
        get => _IsWaiting;
    }

    public void SetWaiting()
    {
        _IsWaiting = true;
    }

    public void SetClicked()
    {
        _IsClicked = true;
        _IsRefreshed = false;
    }

    public void SetReady(bool state)
    {
        _IsReady = state;
    }

    public void Refresh()
    {
        _IsWaiting = false;
        _IsReady = false;
        _IsClicked = false;
        _IsRefreshed = true;
    }
}
