using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ScriptableObject), true)]
public class ScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (serializedObject == null)
            return;

        DrawPropertiesExcluding(serializedObject, "m_Script");

        if (GUI.changed)
            EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

        EditorGUILayout.Separator();
    }

    [MenuItem("Game Settings/Open Global Settings")]
    public static void OpenGlobalSettings()
    {
        EditorUtility.OpenPropertyEditor(GlobalSettings.Get);
    }
}

[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);

        if(property.objectReferenceValue != null)
            Editor.CreateEditor(property.objectReferenceValue).OnInspectorGUI();

        property.serializedObject.ApplyModifiedProperties();
        property.serializedObject.Update();

    }
}