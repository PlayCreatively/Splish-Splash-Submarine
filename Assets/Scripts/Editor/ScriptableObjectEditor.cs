using UnityEngine;
using UnityEditor;
using System.IO;

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

        if (serializedObject.hasModifiedProperties)
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            // save scriptable object
            EditorUtility.SetDirty(target);
            //AssetDatabase.SaveAssets();
        }


        EditorGUILayout.Separator();
    }

    [MenuItem("Game Settings/⚙ Open Global Settings")]
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

[InitializeOnLoad]
public class EditorStartup
{
    static EditorStartup()
    {
        EditorApplication.contextualPropertyMenu += OnPropertyContextMenu;
    }

    ~EditorStartup()
    {
        EditorApplication.contextualPropertyMenu -= OnPropertyContextMenu;
    }

    static void OnPropertyContextMenu(GenericMenu menu, SerializedProperty property)
    {
        // show a custom menu item only for object properties
        if (property.propertyType != SerializedPropertyType.ObjectReference)
            return;

        // and only when called on a ScriptableObject component
        if (property.serializedObject.targetObject.GetType().IsSubclassOf(typeof(ScriptableObject)) == false)
            return;

        string soTypeName = property.type[6..^1];

        menu.AddItem(new GUIContent($"Create new {soTypeName}"), false, () =>
        {
            string rootPath = "Assets/Resources/Settings";
            string path = Path.Combine(rootPath, soTypeName);
            string fullPath = Path.Combine(path, $"new{soTypeName}.asset");

            if (AssetDatabase.IsValidFolder(path) == false)
                AssetDatabase.CreateFolder(rootPath, soTypeName);

            // if file already exists, generate a unique path
            if (File.Exists(fullPath))
                fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);

            var createdSO = ScriptableObject.CreateInstance(soTypeName);
            AssetDatabase.CreateAsset(createdSO, fullPath);

            // select and higlight the new asset
            Selection.activeObject = createdSO;
            EditorGUIUtility.PingObject(createdSO);
        });
    }
}