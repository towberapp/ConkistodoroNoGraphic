using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProgressManager : MonoBehaviour
{
    [SerializeField]
    private GameProgressManager _GameProgressManager = null;

    private SceneSaveData _Progress = null;

    [SerializeField]
    private int _Scene = -1; //if less then 0, then get current scene
    public SceneSaveData Progress
    {
        get
        {
            if (!_Progress)
                Init();
            return _Progress;
        }
    }


    private void Init()
    {
        if (_Scene < 0)
            _Scene = gameObject.scene.buildIndex;
        if (!_GameProgressManager)
            _GameProgressManager = GameProgressManager.Manager;
        _Progress = _GameProgressManager.ScenesData.Find(
                    (SceneSaveData sceneProgress) => sceneProgress.SceneIndex == _Scene);
        if (!_Progress)
        {
            _Progress = new SceneSaveData(_Scene);
            _GameProgressManager.ProgressData.ScenesProgress.Add(_Progress);
        }
    }
    public string GetObjectState(string name)
    {
        if (!Progress.ObjectStates.ContainsKey(name))
            Progress.ObjectStates[name] = "";
        return Progress.ObjectStates[name];
    }

    public void ChangeState(string obj, string state)
    {
        if (!Progress.ObjectStates.ContainsKey(obj))
            Progress.ObjectStates.Add(obj, state);
        else
            Progress.ObjectStates[obj] = state;
    }
}
