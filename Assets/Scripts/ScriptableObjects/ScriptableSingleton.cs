using UnityEngine;

public class ScriptableSingleton<T> : ScriptableObject where T: ScriptableObject
{
    public static T Get => _instance ??= Load();
    static T _instance;

    static readonly string Path = $"Settings/{typeof(T).Name}";
    static readonly string FullPath = "Assets/Resources/" + Path + ".asset";

#if UNITY_EDITOR

    public static void CreateAndShow()
    {
        var so = Load();
        UnityEditor.Selection.activeObject = so;
    }

    static T Load()
    {
        // search if asset exists
        T so = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(FullPath);



        //T so = Resources.Load<T>(Path);

        // if settings is null, create a new one
        if (so == null)
        {
            so = CreateInstance<T>();
            UnityEditor.AssetDatabase.CreateAsset(so, FullPath);
            UnityEditor.AssetDatabase.SaveAssets();
            Debug.Log($"No {typeof(T).Name} found at: {Path}\nCreated a new instance.", so);
        }

        return so;
    }
#else
    static T Load() => Resources.Load<T>(Path);
#endif
}