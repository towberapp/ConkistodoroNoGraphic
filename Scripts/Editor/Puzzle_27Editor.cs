using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Puzzle_27))]
public class Puzzle_27Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Left", GUILayout.Width(50), GUILayout.Height(50)))
            ((Puzzle_27)target).DoorLeft();
        if (GUILayout.Button("Right", GUILayout.Width(50), GUILayout.Height(50)))
            ((Puzzle_27)target).DoorRight();
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Reset", GUILayout.Width(100), GUILayout.Height(100)))
            ((Puzzle_27)target).Reset();
        EditorGUILayout.EndVertical();
    }
}
