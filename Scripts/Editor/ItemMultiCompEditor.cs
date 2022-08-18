using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemMultiComp))]
public class ItemMultiCompEditor : Editor
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
        SerializedProperty compItems =  serializedObject.FindProperty("_CompItems");
        SerializedProperty resultOfComp =  serializedObject.FindProperty("_ResultsOfCompare");
        Color firstColor = new Color(190f / 255, 190f / 255, 190f / 255, 1f);
        resultOfComp.arraySize = compItems.arraySize;
        Rect rect = EditorGUILayout.BeginHorizontal();
        rect.xMin -= 10;
        rect.xMax += 2;
        rect.yMin -= 5;
        //rect.yMax += 2;
        DrawBox(rect, firstColor);
        EditorStyles.wordWrappedLabel.fontSize = 13;
        GUILayout.Label("Compare With", EditorStyles.wordWrappedLabel);
        GUILayout.Space(50);
        GUILayout.Label("Result", EditorStyles.wordWrappedLabel);
        //GUILayout.Label("\n");
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
        rect = EditorGUILayout.BeginVertical();
        rect.xMin -= 10;
        rect.yMin -= 3;
        rect.yMax += 3;
        rect.xMax += 2;
        DrawBox(rect, new Color(210f / 255, 210f / 255, 210f / 255, 1));
        for (int i = 0; i < compItems.arraySize; i++)
        {
            SerializedProperty sceneEl = compItems.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginHorizontal();
            if (i == 0)
                rect.yMin -= 5;
            rect.xMin -= 10;
            EditorGUILayout.PropertyField(sceneEl, GUIContent.none);
            serializedObject.ApplyModifiedProperties();
            SerializedProperty clipEl = resultOfComp.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(clipEl, GUIContent.none);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    public override void OnInspectorGUI()
    {

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_Name"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_Sprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_SpriteScaler"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_isShow"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_AnimationInfo"));
        var isComp = serializedObject.FindProperty("_IsComp");
        EditorGUILayout.PropertyField(isComp);
        serializedObject.ApplyModifiedProperties();

        if (isComp.boolValue)
        {
            GUILayout.Label("\n");

            Draw();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(40), GUILayout.Height(40)))
            {
                serializedObject.FindProperty("_CompItems").arraySize++;
                serializedObject.ApplyModifiedProperties();
            }
            if (GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(40), GUILayout.Height(40)))
            {
                serializedObject.FindProperty("_CompItems").arraySize--;
                serializedObject.ApplyModifiedProperties();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}