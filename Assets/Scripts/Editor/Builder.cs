
using UnityEditor;
using UnityEngine;

class Builder
{
    const string 
        path = "Builds/webGL/",
        pushCMD = "butler-windows/push.cmd",
        outputPath = "webGL-setup/";

    [MenuItem("Tools/🛠+⏩ | Build and Push", priority = 1)]
    public static void Build()
    {
        // Search for all scenes in the project.
        string[] levels = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = EditorBuildSettings.scenes[i].path;
        }

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "", BuildTarget.WebGL, BuildOptions.None);

        // Copy the Build folder to the webGL-setup folder.
        const string toCopy = "Build";
        FileUtil.ReplaceDirectory(path + toCopy, outputPath + toCopy);

        Push();
    }

    [MenuItem("Tools/⏩ | Push to Itch", priority = -1)]
    public static void Push()
    {
        if (SystemInfo.operatingSystem.Contains("Windows") == false)
            Debug.Log("This operation only works on Windows. You are using " + SystemInfo.operatingSystem);

        //// reveal push.cmd in explorer
        //EditorUtility.RevealInFinder(Application.dataPath.Replace("Assets", "") + pushCMD);

        // execute push.cmd
        EditorUtility.OpenWithDefaultApp(Application.dataPath.Replace("Assets", "") + pushCMD);
    }
}

