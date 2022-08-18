using UnityEngine;
using System.Collections.Generic;

public static class ResourcesLoader
{
    public static bool TryGetAsset<T>(ref T result, string fullPath) where T : Object
    {
        var newItem = Resources.Load(fullPath);
        if (newItem && newItem is T)
        {
            result = (T)newItem;
            Debug.Log($"Resources loader: {fullPath} loaded!");
            return true;
        }
        Debug.Log($"Resources loader: {fullPath} not found or wrong type!");
        return false;
    }
    public static T[] GetAssets<T>(string path, string[] fileNames) where T : Object
    {
        List<T> result = new List<T>();
        for (int i = 0; i < fileNames.Length; i++)
            if ((Resources.Load(path + fileNames[i]) is T obj))
                result.Add(obj);
        return result.ToArray();
    }
}
