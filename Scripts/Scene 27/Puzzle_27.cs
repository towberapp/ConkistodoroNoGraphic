using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_27 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private State _DoorState;
    [SerializeField] private PuzzleLights_27 _Light = null;
    [SerializeField] private List<PuzzleDoor_27> _Doors = new List<PuzzleDoor_27>();
    [SerializeField] private PuzzleInner_27 _InnerPart = null;
    private int _doorIndex = 0;

    private int _DoorIndex
    {
        get => _doorIndex;
        set
        {
            _doorIndex = value;
            _Light.SetLamp(_doorIndex + 1);
        }
    }
    private void Start()
    {
        _Light.SetLamp(1);
    }

    public void SetDoorsOpen()
    {
        foreach (var door in _Doors)
            door.Open();
        _DoorIndex = _Doors.Count;
    }

    public void DoorRight()
    {
        if (_DoorIndex >= _Doors.Count)
            return;
        _Doors[_DoorIndex++].MoveRight();
        CheckDoors();
    }

    public void DoorLeft()
    {
        if (_DoorIndex >= _Doors.Count)
            return;
        _Doors[_DoorIndex++].MoveLeft();
        CheckDoors();
    }

    public void Reset()
    {
        foreach (var door in _Doors)
            door.Reset();
        _DoorIndex = 0;
        CheckDoors();
    }

    private void CheckDoors()
    {
        foreach (var door in _Doors)
        {
            if (!door.IsCorrect)
            {
                _DoorState.Value = "Closed";
                return;
            }
        }
        _DoorState.Value = "Opened";
    }
}
