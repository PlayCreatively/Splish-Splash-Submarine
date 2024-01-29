using System;
using UnityEngine;

public class GlobalSettings : ScriptableObject
{
    public static GlobalSettings Get => (_instance != null) 
        ? _instance
        : (_instance = LoadGameSettings());
    static GlobalSettings _instance;

    const string Path = "Settings/GlobalSettings";
    const string FullPath = "Assets/Resources/" + Path + ".assets";
    public static ScriptableObject Load(string name) => Resources.Load<ScriptableObject>("Settings/" + name);
    public static GameSettings Current => Get._current;
    [Tooltip("The currently selected game mode setting. This is the one that will be used in the game.\nSelect a different one for a different game style.")]
    public GameSettings _current;

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Settings")]
    public static void OpenSettings()
    {
        UnityEditor.Selection.activeObject = Get;
    }

    static GlobalSettings LoadGameSettings()
    {
        GlobalSettings settings = Resources.Load<GlobalSettings>(Path);

        // if settings is null, create a new one
        if (settings == null)
        {
            settings = CreateInstance<GlobalSettings>();
            UnityEditor.AssetDatabase.CreateAsset(settings, Path);
            UnityEditor.AssetDatabase.SaveAssets();
            Debug.LogError($"No {nameof(GlobalSettings)} found at: {Path}\nCreated a new instance.", settings);
        }

        return settings;
    }
#else
    static GlobalSettings LoadGameSettings() => Resources.Load<GlobalSettings>(Path);
#endif
}

public class SettingsBase<T> : ScriptableObject where T : SettingsBase<T>
{
    public Action<T> onValidate;

    void OnValidate()
    {
        onValidate?.Invoke((T)this);
    }
}