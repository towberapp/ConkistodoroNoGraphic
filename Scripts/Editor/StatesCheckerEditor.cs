using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StatesChecker))]
public class StatesCheckerEditor : Editor
{
    private Texture2D _texture;

    private void DrawBox(Rect position, Color color)
    {
        if (!_texture)
            _texture = new Texture2D(1, 1);
        if (_texture.GetPixel(0, 0) != color)
        {
            _texture.SetPixel(0, 0, color);
            _texture.Apply();
        }
        GUI.skin.box.normal.background = _texture;
        GUI.Box(position, GUIContent.none, EditorStyles.helpBox);
    }
    private void Draw()
    {
        SerializedProperty events = serializedObject.FindProperty("_TargetEvents");
        SerializedProperty states = serializedObject.FindProperty("_StatesForCheck");
        SerializedProperty targetValues = serializedObject.FindProperty("_TargetValues");
        SerializedProperty items = serializedObject.FindProperty("_Items");
        SerializedProperty clips = serializedObject.FindProperty("_Clips");
        targetValues.arraySize = events.arraySize;
        clips.arraySize = events.arraySize;
        items.arraySize = events.arraySize;
        Color firstColor = new Color(190f / 255, 190f / 255, 190f / 255, 1f);
        Rect rect = EditorGUILayout.BeginHorizontal();
        rect.xMin -= 10;
        rect.xMax += 2;
        rect.yMin -= 5;
        //rect.yMax += 2;
        DrawBox(rect, firstColor);
        EditorStyles.wordWrappedLabel.fontSize = 13;
        GUILayout.Label("States value", EditorStyles.wordWrappedLabel);
        GUILayout.Space(50);
        GUILayout.Label("Interact event", EditorStyles.wordWrappedLabel);
        //GUILayout.Label("\n");
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
        rect = EditorGUILayout.BeginVertical();
        rect.xMin -= 10;
        rect.yMin -= 3;
        rect.yMax += 3;
        rect.xMax += 2;
        DrawBox(rect, new Color(210f / 255, 210f / 255, 210f / 255, 1));
        for (int i = 0; i < events.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Timeline Clip: ", GUILayout.MaxWidth(80), GUILayout.MinWidth(10));
            EditorGUILayout.PropertyField(clips.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.MaxWidth(120), GUILayout.MinWidth(10), GUILayout.Height(15));
            EditorGUILayout.LabelField("Item: ", GUILayout.MaxWidth(80), GUILayout.MinWidth(10));
            EditorGUILayout.PropertyField(items.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.MaxWidth(120), GUILayout.MinWidth(10), GUILayout.Height(15));
            if (GUILayout.Button("Remove", EditorStyles.miniButtonRight, GUILayout.Width(60), GUILayout.Height(15)))
            {
                events.DeleteArrayElementAtIndex(i);
                clips.DeleteArrayElementAtIndex(i);
                targetValues.DeleteArrayElementAtIndex(i);
                items.DeleteArrayElementAtIndex(i);
                serializedObject.ApplyModifiedProperties();
                return;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            string newTarget = "";
            string[] oldValues = targetValues.GetArrayElementAtIndex(i).stringValue.Split('\n');

            rect = EditorGUILayout.BeginVertical(GUILayout.MaxWidth(100), GUILayout.MinWidth(10), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
            rect.x -= 2;
            rect.height += 2;
            rect.width += 5;
            DrawBox(rect, new Color(160f / 255, 160f / 255, 180f / 255, 1));
            for (int j = 0; j < states.arraySize; j++)
            {
                EditorGUILayout.LabelField(states.GetArrayElementAtIndex(j)?.objectReferenceValue?.name, GUILayout.MaxWidth(100), GUILayout.MinWidth(10), GUILayout.ExpandWidth(false));
                newTarget += EditorGUILayout.TextField(j < oldValues.Length ? oldValues[j] : "", GUILayout.MaxWidth(100), GUILayout.MinWidth(10), GUILayout.ExpandWidth(false));
                newTarget += '\n';
            }
            newTarget += (items.GetArrayElementAtIndex(i)?.objectReferenceValue?.name ?? "none");
            targetValues.GetArrayElementAtIndex(i).stringValue = newTarget;
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
            SerializedProperty eventEl = events.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(eventEl, GUILayout.ExpandWidth(true));
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();
            if (i < events.arraySize - 1)
                EditorGUILayout.LabelField("\n");
        }
        EditorGUILayout.EndVertical();
    }

    public override void OnInspectorGUI()
    {

        SerializedProperty checkStates = serializedObject.FindProperty("_StatesForCheck");
        EditorGUILayout.PropertyField(checkStates.FindPropertyRelative("Array.size"));
        serializedObject.ApplyModifiedProperties();
        for (int i = 0; i < checkStates.arraySize; i++)
            EditorGUILayout.PropertyField(checkStates.GetArrayElementAtIndex(i));


        GUILayout.Label("\n");

        Draw();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(40), GUILayout.Height(40)))
        {
            serializedObject.FindProperty("_TargetEvents").arraySize++;
            serializedObject.ApplyModifiedProperties();
        }
        if (GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(40), GUILayout.Height(40)))
        {
            serializedObject.FindProperty("_TargetEvents").arraySize--;
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.EndHorizontal();
    }
}