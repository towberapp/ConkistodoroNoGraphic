using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemAnimation))]
public class ItemAnimEditor : Editor
{
    private Texture2D _texture;

    private SerializedProperty _ObjectsPath = null;
    private SerializedProperty _NecesseryObjects = null;

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
        _ObjectsPath = serializedObject.FindProperty("_ObjectsPath");
        _NecesseryObjects = serializedObject.FindProperty("_NecesseryObjects");
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

        rect = EditorGUILayout.BeginVertical();
        rect.xMin -= 10;
        rect.yMin -= 3;
        rect.yMax += 3;
        rect.xMax += 2;
        DrawBox(rect, new Color(210f / 255, 210f / 255, 210f / 255, 1));

        _NecesseryObjects.arraySize = _ObjectsPath.arraySize;
        for (int i = 0; i < _ObjectsPath.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Parent Object: ", GUILayout.MaxWidth(80), GUILayout.MinWidth(10));
            EditorGUILayout.LabelField("Extra Objects: ", GUILayout.MaxWidth(80), GUILayout.MinWidth(10));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_ObjectsPath.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.MaxWidth(120), GUILayout.MinWidth(10), GUILayout.Height(15));
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(_NecesseryObjects.GetArrayElementAtIndex(i), true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.EndVertical();
    }

    public override void OnInspectorGUI()
    {
        _ObjectsPath = serializedObject.FindProperty("_ObjectsPath");
        _NecesseryObjects = serializedObject.FindProperty("_NecesseryObjects");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_AnimName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_PlayerSide"));
        serializedObject.ApplyModifiedProperties();
        


        GUILayout.Label("\n");
        Draw();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(40), GUILayout.Height(40)))
        {
            _ObjectsPath.arraySize++;
            serializedObject.ApplyModifiedProperties();
        }
        if (_ObjectsPath.arraySize > 0 && GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(40), GUILayout.Height(40)))
        {
            _ObjectsPath.arraySize--;
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.EndHorizontal();
    }
}