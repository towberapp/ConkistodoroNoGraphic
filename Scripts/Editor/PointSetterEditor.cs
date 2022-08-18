using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PointSetter))]
public class PointSetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SerializedProperty _target = serializedObject.FindProperty("_Target");
        EditorGUILayout.PropertyField(_target);
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Set to Target", GUILayout.Width(80), GUILayout.Height(60)))
            ((PointSetter)target).SetToTarget();
    }
}
