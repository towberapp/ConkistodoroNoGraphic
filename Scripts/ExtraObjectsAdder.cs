using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraObjectsAdder : MonoBehaviour
{
    [SerializeField]
    private LayerChecker _LayerChecker = null;
    private List<GameObject> _ExtraObjects = new List<GameObject>();

    private GameObject FindChild(string name)
    {
        Transform[] childs = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in childs)
            if (child.name == name)
                return child.gameObject;
        return null;
    }
    private GameObject[] FindChilds(string[] objects)
    {
        List<GameObject> result = new List<GameObject>();
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            foreach (string name in objects)
                if (name == child.name)
                    result.Add(child.gameObject);
        }
        return result.ToArray();
    }

    public void AddExtraObjects(string parentName, GameObject[] extraObjects)
    {
        Transform parent = FindChild(parentName).transform;
        foreach (GameObject extra in extraObjects)
        {
            if (_ExtraObjects.Find((el) => el.name == extra.name) == null)
            {
                GameObject newObject = Instantiate(extra, parent);
                _LayerChecker?.HideObject(newObject.GetComponent<SpriteRenderer>());
                newObject.name = extra.name;
                _ExtraObjects.Add(newObject);
            }
        }
    }
    public void RemoveExtraObjects(GameObject[] extraObjects)
    {
        string[] names = new string[extraObjects.Length];
        for (int i = 0; i < extraObjects.Length; i++)
            names[i] = extraObjects[i].name;
        List<GameObject> trashObjects = new List<GameObject>();
        foreach (var extra in extraObjects)
            if (_ExtraObjects.Find((el) => el.name == extra.name) is var trash && trash != null)
                trashObjects.Add(trash);
        foreach (GameObject trash in trashObjects)
        { 
            _ExtraObjects.Remove(trash);
            Destroy(trash);
        }
    }

    public void RemoveUnnecessary(List<AnimationExtraObjects> data)
    {
        List<GameObject> unnecessary = new List<GameObject>();
        List<string> names = new List<string>();
        foreach (var objects in data)
            foreach (var obj in objects.Objects)
                names.Add(obj.name);
        foreach (var extra in _ExtraObjects)
            if (!names.Contains(extra.name))
                unnecessary.Add(extra);
        RemoveExtraObjects(unnecessary.ToArray());
    }

    public void ClearAllExtra()
    {
        if (_ExtraObjects.Count > 0)
            RemoveExtraObjects(_ExtraObjects.ToArray());
    }
}
