using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    private GameProgressManager _Progress = null;

    [SerializeField]
    private string _FileName = "save_01";

    void Start()
    {
        _Progress = GameProgressManager.Manager;
    }

    public void Save()
    {
        _Progress.ProgressData.LastScene = gameObject.scene.buildIndex;
        _Progress.SetFileName(_FileName);
        SaveManager.Save(_Progress.ProgressData, "save_01");
    }
}
