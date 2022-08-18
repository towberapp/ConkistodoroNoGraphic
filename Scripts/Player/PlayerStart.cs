using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(fileName = "PlayerStart", menuName = "", order = 60)]
public class PlayerStart : ScriptableObject
{
    [SerializeField]
    public string StartPointName = "";
    [SerializeField]
    public PlayerSide StartSide = null;
    public RuntimeAnimatorController AnimController = null;
    [HideInInspector]
    public MapPoint StartPoint = null;
/*
    public string StartPointName
    {
        get => _StartPointName;
        set
        {
            _StartPointName = value;
        } 
    }

    public PlayerSide StartSide
    {
        get => _StartSide;
        set
        {
            _StartSide = value;
        }
    }
*/
}
