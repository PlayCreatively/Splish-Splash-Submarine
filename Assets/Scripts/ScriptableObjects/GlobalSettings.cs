using System;
using UnityEngine;

public class GlobalSettings : ScriptableSingleton<GlobalSettings>
{
    public static ModeSettings Current => Get._current;
    [Tooltip("The currently selected game mode setting. This is the one that will be used in the game.\nSelect a different one for a different game style.")]
    public ModeSettings _current;

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Find/" + nameof(GlobalSettings))]
    public static new void CreateAndShow()
    {
        ScriptableSingleton<GlobalSettings>.CreateAndShow();
    }
#endif

}

public class SettingsBase<T> : ScriptableObject where T : SettingsBase<T>
{
    public Action<T> onValidate;

    public const string Path = "Setting Objects/";

    void OnValidate()
    {
        onValidate?.Invoke((T)this);
    }
}