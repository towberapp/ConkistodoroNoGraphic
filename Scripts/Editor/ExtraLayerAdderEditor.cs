using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ExtraLayersAdder))]
public class ExtraLayerAdderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Refresh", GUILayout.Width(100), GUILayout.Height(50)))
            ((ExtraLayersAdder)target).UpdateRenderes();
        if (GUILayout.Button("+", GUILayout.Width(25), GUILayout.Height(25)))
            ((ExtraLayersAdder)target).AddExtraLayers();
        if (GUILayout.Button("-", GUILayout.Width(25), GUILayout.Height(25)))
            ((ExtraLayersAdder)target).RemoveExtraLayers();
        EditorGUILayout.EndHorizontal();
    }
}
