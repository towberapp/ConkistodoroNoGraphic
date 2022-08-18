using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 52)]
public class Item : ScriptableObject
{
    [SerializeField]
    private string _Name = "";

    [SerializeField]
    private Sprite _Sprite = null;

    [SerializeField]
    private bool _IsComp = false;

    [SerializeField]
    private Item _CompareWith = null;

    [SerializeField]
    private Item _ResultOfComp = null;

    [SerializeField]
    private float _SpriteScaler = 0;

    [SerializeField]
    private bool _isShow = true;

    [SerializeField]
    private ItemAnimation _AnimationInfo;

    [SerializeField]
    private GameEvent _onClickEvent;
    public bool IsShow
    {
        get
        {
            return _isShow;
        }
    }
    public float SpriteScaler
    {
        get
        {
            return _SpriteScaler;
        }
    }
    public string Name 
    { 
        get
        {
            return _Name;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return _Sprite;
        }
    }

    public Item CompareWith
    {
        get
        {
            return _CompareWith;
        }
    }

    public Item ResultOfComp
    {
        get
        {
            return _ResultOfComp;
        }
    }

    public bool IsComparable
    {
        get
        {
            return _IsComp;
        }
    }

    public ItemAnimation AnimationInfo
    {
        get => _AnimationInfo;
    }

    public GameEvent OnClickEvent
    {
        get => _onClickEvent;
    }

    public virtual Item Compare(Item item)
    {
        if (_IsComp && _CompareWith == item)
            return _ResultOfComp;
        return null;
    }
}
