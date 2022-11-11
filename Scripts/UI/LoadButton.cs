using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    [SerializeField]
    private string _FileName = "save_01";

    private GameProgressManager _Progress = null;
    private SceneManipulator _SceneManipulator = null;
    void Start()
    {
        _SceneManipulator = SceneManipulator.Manipulator;
        _Progress = GameProgressManager.Manager;
    }

    public void LoadSave()
    {
        _Progress.LoadFile(_FileName);
        _SceneManipulator.LoadFromSave(_Progress.ProgressData.LastScene);
    }

    public void LoadNextScene(int i)
    {
        //_SceneManipulator.LoadFromScene(i);
        StartCoroutine(LoadScene(i));
    }

    IEnumerator LoadScene(int i)
    {
        yield return new WaitForSeconds(1.0f);
        _SceneManipulator.LoadFromScene(i);
    }
}
