using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ScriptableObject), true)]
public class ScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI() => DrawScriptableObject(serializedObject);

    void DrawScriptableObject(SerializedObject serializedObject)
    {
        EditorGUI.indentLevel++;

        SerializedProperty serializedProperty = serializedObject.GetIterator();
        serializedProperty.NextVisible(true);

        while (serializedProperty.NextVisible(false))
            if (serializedProperty.name == "m_Script")
                continue;
            else if (serializedProperty.propertyType == SerializedPropertyType.ObjectReference
                && serializedProperty.objectReferenceValue != null)
            {
                EditorGUILayout.PropertyField(serializedProperty, true);

                DrawScriptableObject(new(serializedProperty.objectReferenceValue));
            }
            else
                EditorGUILayout.PropertyField(serializedProperty, true);

        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}