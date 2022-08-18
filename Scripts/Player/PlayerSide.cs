using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSide",menuName = "ScriptableObjects/PlayerSide", order = 51)]
public class PlayerSide : ScriptableObject
{
    [SerializeField]
    private string _SideName = null;

    [SerializeField]
    private string _AnimName = null;

    [SerializeField]
    private Vector2 _MoveVector = Vector2.zero;

    public Vector2 MoveVector
    {
        get => _MoveVector;
    }

    public string AnimationName
    {
        get
        {
            return _AnimName;
        }
    }
    public string SideName
    {
        get => _SideName;
    }
}
