using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Comic", menuName = "Comic")]
public class ComicAsset : ScriptableObject
{
    public List<Panel> panels;

    public static ComicAsset Load(int index) => Resources.Load<ComicAsset>($"Comics/Comic {index}");
}

[Serializable]
public struct Panel
{
    public Sprite sprite;
    public AudioClip music;
}

#if UNITY_EDITOR
// custom property drawer for Panel without foldout
[CustomPropertyDrawer(typeof(Panel))]
public class PanelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.width *= 0.5f;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("sprite"), GUIContent.none);
        position.x += position.width;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("music"), GUIContent.none);
    }
}
#endif