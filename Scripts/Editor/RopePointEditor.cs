using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Point_19))]
public class RopePointEditor : Editor
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
        SerializedProperty ropes =  serializedObject.FindProperty("_Ropes");
        SerializedProperty clips =  serializedObject.FindProperty("_RopeClips");
        Color firstColor = new Color(190f / 255, 190f / 255, 190f / 255, 1f);
        if (ropes.arraySize != 3)
            ropes.arraySize = 3;
        clips.arraySize = ropes.arraySize;
        Rect rect = EditorGUILayout.BeginHorizontal();
        rect.xMin -= 10;
        rect.xMax += 2;
        rect.yMin -= 5;
        //rect.yMax += 2;
        DrawBox(rect, firstColor);
        EditorStyles.wordWrappedLabel.fontSize = 13;
        GUILayout.Label("Rope", EditorStyles.wordWrappedLabel);
        GUILayout.Space(50);
        GUILayout.Label("Clip", EditorStyles.wordWrappedLabel);
        //GUILayout.Label("\n");
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
        rect = EditorGUILayout.BeginVertical();
        rect.xMin -= 10;
        rect.yMin -= 3;
        rect.yMax += 3;
        rect.xMax += 2;
        DrawBox(rect, new Color(210f / 255, 210f / 255, 210f / 255, 1));
        for (int i = 0; i < ropes.arraySize; i++)
        {
            SerializedProperty sceneEl = ropes.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginHorizontal();
            if (i == 0)
                rect.yMin -= 5;
            rect.xMin -= 10;
            EditorGUILayout.PropertyField(sceneEl, GUIContent.none);
            serializedObject.ApplyModifiedProperties();
            SerializedProperty clipEl = clips.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(clipEl, GUIContent.none);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    public override void OnInspectorGUI()
    {

        SerializedProperty defaultClip = serializedObject.FindProperty("_Name");
        EditorGUILayout.PropertyField(defaultClip);
        serializedObject.ApplyModifiedProperties();

        GUILayout.Label("\n");

        Draw();
    }
}