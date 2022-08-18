using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManipulator : MonoBehaviour
{
    [SerializeField]
    private int _LastScene = 0,
        _CurrentScene = 0,
        _NextScene = 0;

    private bool _IsReady = false;
    private Coroutine _Loading = null;

    private static SceneManipulator _Manipulator;

    public static SceneManipulator Manipulator
    {
        get
        {
            if (!_Manipulator)
                Init();
            return _Manipulator;
        }
    }
    public int Last
    {
        get => _LastScene;
        private set => _LastScene = value;
    }

    public int Current
    {
        get => _CurrentScene;
        private set => _CurrentScene = value;
    }

    public int Next
    {
        get => _NextScene;
        private set => _NextScene = value;
    }
    private static void Init()
    {
        _Manipulator = FindObjectOfType<SceneManipulator>();
        if (!_Manipulator)
            _Manipulator = Instantiate<GameObject>(new GameObject(), null).AddComponent<SceneManipulator>();
        _Manipulator.Current = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(_Manipulator);
    }
    private IEnumerator Loading(int scene)
    {
        if (Application.backgroundLoadingPriority != ThreadPriority.BelowNormal)
            Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
        AsyncOperation async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        while (!_IsReady)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
        Current = scene;
        _IsReady = false;
        _Loading = null;
    }

    public void EnableSwitching() => _IsReady = true;

    public void LoadFromSave(int scene)
    {
        Next = scene;
        Last = -1;
        Load(scene);
    }
    private void Load(int scene)
    {
        if (Application.backgroundLoadingPriority != ThreadPriority.BelowNormal)
            Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
        if (_Loading == null)
            _Loading = StartCoroutine(Loading(scene));
    }
    public void LoadFromScene(int scene)
    {
        Next = scene;
        Last = Current;
        Load(scene);
    }
}
