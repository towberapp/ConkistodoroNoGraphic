using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterDebug : MonoBehaviour
{
    public void StringDebug(string text)
    {
        string name = gameObject.name;
        Debug.LogFormat("{0} -> {1}", name, text);
    }
}
