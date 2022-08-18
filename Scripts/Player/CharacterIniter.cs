using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIniter : MonoBehaviour
{
    private PlayerStart _start = null;

    public PlayerStart Start
    {
        get
        {
            if (!_start)
            {
                Init(SceneManipulator.Manipulator.Last);
            }
            return _start;
        }
    }

    [SerializeField]
    private List<PlayerStart> _PlayerStarts = new List<PlayerStart>();

    [SerializeField]
    private List<int> _Scenes = new List<int>();

    private IEnumerator LoadSide(ResourceRequest loader)
    {
        while (!loader.isDone)
            yield return null;
        if (loader.asset is PlayerSide)
            _start.StartSide = (PlayerSide)loader.asset;
    }

    public void ChangeStart(int scene, PlayerStart newStart)
    {
        int i = _Scenes.FindIndex(0, (a) => a == scene);
        if (i != -1)
            _PlayerStarts[i] = newStart;
    }
    private IEnumerator LoadAnimator(ResourceRequest loader)
    {
        while (!loader.isDone)
            yield return null;
        if (loader.asset is RuntimeAnimatorController)
            _start.AnimController = (RuntimeAnimatorController)loader.asset;
    }
    public void Init(int prevScene)
    {
        int index = _Scenes.FindIndex((int i) => i == prevScene);
        if (index == -1)
        {
            _start = ScriptableObject.Instantiate<PlayerStart>(_PlayerStarts[0]);
            ScriptableObject.Destroy(_start, 10);
            _start.StartPointName = GameProgressManager.Manager.PlayerData.LastPoint;
            //StartCoroutine(LoadSide(Resources.LoadAsync($"Sides/{GameProgressManager.Manager.PlayerData.LastSide}")));
            if (Resources.Load($"Sides/{GameProgressManager.Manager.PlayerData.LastSide}") is PlayerSide startSide)
                _start.StartSide = startSide;
            //StartCoroutine(LoadAnimator(Resources.LoadAsync($"AnimControllers/{GameProgressManager.Manager.PlayerData.Animator}")));
            if (Resources.Load($"AnimControllers/{GameProgressManager.Manager.PlayerData.Animator}") is RuntimeAnimatorController startController)
                _start.AnimController = startController;
        }
        else
        {
            _start = _PlayerStarts[index];
        }
        foreach (MapPoint point in FindObjectsOfType<MapPoint>())
        {
            if (point.name == _start.StartPointName)
            {
                _start.StartPoint = point;
                break;
            }
        }
        Debug.Log($"Inited: {_start.StartPointName} " +
        $"{_start.StartSide.name}");
    }
}
