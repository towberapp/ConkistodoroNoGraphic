using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProgressManager : MonoBehaviour
{
    private PlayerSaveData _PlayerSave = null;
    [SerializeField]
    private PointSystem _PointSystem = null;

    [SerializeField]
    private Item[] _Inventory = null;

    [SerializeField]
    private MapPoint _LastPoint = null;

    [SerializeField]
    private PlayerSide _LastSide = null;

    [SerializeField]
    private RuntimeAnimatorController _AnimController = null;

    [SerializeField]
    private GameProgressManager _GameProgress = null;

    private PlayerSaveData PlayerSave
    {
        get
        {
            if (_PlayerSave)
                Init();
            return _PlayerSave;
        }
        set
        {
            _PlayerSave = value;
        }
    }

    public Item[] Inventory
    {
        get
        {
            if (_Inventory.Length == 0)
                _Inventory = ResourcesLoader.GetAssets<Item>("Items/", PlayerSave.Inventory);
            return _Inventory;
        }
        set
        {
            _Inventory = value;
            UpdateInventory(value);
        }
    }

    public PlayerSide LastSide
    {
        get
        {
            if (!_LastSide)
            {
                string path = "Sides/" + PlayerSave.LastSide;
                ResourcesLoader.TryGetAsset<PlayerSide>(ref _LastSide, path);
            }
            return _LastSide;
        }
        set
        {
            PlayerSave.LastSide = value.SideName;
            _LastSide = value;
        }
    }
    public MapPoint LastPoint
    {
        get
        {
            if (!_LastPoint)
            {
                _LastPoint = _PointSystem.GetPointByName(PlayerSave.LastPoint);
            }
            return _LastPoint;
        }
        set
        {
            PlayerSave.LastPoint = value.name;
            _LastPoint = value;
        }
    }

    public RuntimeAnimatorController AnimController
    {
        get
        {
            if (!_AnimController)
                ResourcesLoader.TryGetAsset(ref _AnimController, "AnimatorControllers/" + PlayerSave.Animator);
            return _AnimController;
        }
        set
        {
            PlayerSave.Animator = value.name;
            _AnimController = value;
        }
    }
    private void Init()
    {
        if (!_GameProgress)
            _GameProgress = GameProgressManager.Manager;
        _PlayerSave = _GameProgress.PlayerData;
    }

    private  void UpdateInventory(Item[] newInventory)
    {
        PlayerSave.Inventory = new string[newInventory.Length];
        for (int i = 0; i < newInventory.Length; i++)
            PlayerSave.Inventory[i] = newInventory[i].Name;
    }
}
