using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager_21 : MonoBehaviour
{
    [SerializeField] private BlackScreen _blackScreen;
    private int _connectedScene;

    void Start()
    {
        _connectedScene = SceneManipulator.Manipulator.Last;
    }

    public void Exit()
    {
        SceneManipulator.Manipulator.LoadFromScene(_connectedScene);
        _blackScreen.Appear();
    }
}
