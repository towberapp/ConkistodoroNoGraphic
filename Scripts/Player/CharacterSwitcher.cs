using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject _MainCharacter = null;
    
    [SerializeField]
    private List<GameObject> _TimelineCharacters = null;

    private bool _IsSwitching = false;

    private IEnumerator Switching(GameObject toDisable, GameObject toEnable)
    {
        while (_IsSwitching)
            yield return null;
        _IsSwitching = true;
        toEnable.SetActive(true);
        while (!toEnable.activeSelf)
            yield return null;
        toDisable.SetActive(false);
        _IsSwitching = false;
    }

    private IEnumerator Switching(GameObject toDisable, List<GameObject> toEnable)
    {
        while (_IsSwitching)
            yield return null;
        _IsSwitching = true;
        bool isReady = false;
        foreach (GameObject obj in toEnable)
            obj.SetActive(true);
        while (!isReady)
        {
            isReady = true;
            foreach (GameObject obj in toEnable)
                if (!obj.activeSelf)
                    isReady = false;
            yield return null;
        }
        toDisable.SetActive(false);
        _IsSwitching = false;
    }

    private IEnumerator Switching(List<GameObject> toDisable, GameObject toEnable)
    {
        while (_IsSwitching)
            yield return null;
        _IsSwitching = true;
        toEnable.SetActive(true);
        while (!toEnable.activeSelf)
            yield return null;
        foreach (GameObject obj in toDisable)
            obj.SetActive(false);
        _IsSwitching = false;
    }
    public void SetMain()
    {
        StartCoroutine(Switching(_TimelineCharacters, _MainCharacter));
    }

    public void SetTimeline()
    {
        StartCoroutine(Switching(_MainCharacter, _TimelineCharacters));
    }
}
