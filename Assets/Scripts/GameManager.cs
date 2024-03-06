using UnityEngine.SceneManagement;

public class GameManager : ScriptableSingleton<GameManager>
{
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Find/" + nameof(GameManager))]
    public static new void CreateAndShow()
    {
        ScriptableSingleton<GameManager>.CreateAndShow();
    }
#endif
}