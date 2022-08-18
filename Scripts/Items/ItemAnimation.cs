using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationExtraObjects
{
    public List<GameObject> Objects = new List<GameObject>();

    public GameObject this[int key]
    {
        get
        {
            return Objects[key];
        }
        set
        {
            Objects[key] = value;
        }
    }

    public static explicit operator List<GameObject>(AnimationExtraObjects extra) => extra.Objects;
    public static implicit operator GameObject[](AnimationExtraObjects extra) => extra.Objects.ToArray();
}
[CreateAssetMenu(fileName = "ItemAnimation", menuName = "ItemAnimation", order = 52)]
public class ItemAnimation : ScriptableObject
{
    [SerializeField] private List<GameObject> _ObjectsPath = new List<GameObject>();
    [SerializeField] private List<AnimationExtraObjects> _NecesseryObjects = new List<AnimationExtraObjects>();
    [SerializeField] private string _AnimName = "";
    [SerializeField] private PlayerSide _PlayerSide;
    public List<GameObject> ObjectsPath
    {
        get
        {
            return _ObjectsPath;
        }
    }

    public List<AnimationExtraObjects> NecesseryObjects
    {
        get
        {
            return _NecesseryObjects;
        }
    }

    public string AnimationName
    {
        get
        {
            return _AnimName;
        }
    }

    public PlayerSide PlayerSide
    {
        get => _PlayerSide;
    }
}

