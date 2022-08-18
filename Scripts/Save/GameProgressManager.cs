using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameProgressManager : MonoBehaviour
{
    private GameProgressData _ProgressData = null;

    private string _SaveFile = "Save_1";

    private static GameProgressManager _Manager = null;

    public static GameProgressManager Manager
    {
        get
        {
            if (!_Manager)
                _Manager = Find();
            return _Manager;
        }
    }
    private void Start()
    {
        if (GameProgressManager.Manager && GameProgressManager.Manager != this)
            Destroy(gameObject);
        else 
            DontDestroyOnLoad(Manager);
    }

    private static GameProgressManager Find()
    {
        GameProgressManager result;
        result = FindObjectOfType<GameProgressManager>();
        if (!result)
            result = Instantiate<GameObject>(new GameObject()).AddComponent<GameProgressManager>();
        return result;
    }

    public GameProgressData ProgressData
    {
        get
        {
            if (!_ProgressData)
                Init();
            return _ProgressData;
        }
    }

    public List<SceneSaveData> ScenesData
    {
        get => ProgressData.ScenesProgress;
    }

    public PlayerSaveData PlayerData
    {
        get => ProgressData.PlayerProgress;
    }

    public string Point;

    private void Init()
    {
        Save loadedSave = SaveManager.Load(_SaveFile);
        if (loadedSave && loadedSave is GameProgressData)
        {
            _ProgressData = (GameProgressData)loadedSave;
            Debug.Log("Save Data: OK!");
            return;
        }
        else if (loadedSave)
            Debug.Log("Save Data: Wrong!");
        _ProgressData = new GameProgressData();
        Point = _ProgressData.PlayerProgress.LastPoint;
    }

    public void SetFileName(string newFileName) => _SaveFile = newFileName != "" ? newFileName : _SaveFile;

    public void SetFileName(int newFileIndex) => _SaveFile = $"Save_{newFileIndex}";

    public void LoadFile(int fileIndex)
    {
        SetFileName(fileIndex);
        _ProgressData = null;
        Init();
    }
    public void LoadFile(string fileName)
    {
        SetFileName(fileName);
        Init();
    }
}
