using UnityEngine.SceneManagement;

public class GameManager : ScriptableSingleton<GameManager>
{
    
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    
    public static void LoadScene(SceneType type)
    {
        SceneManager.LoadScene((int)type);
    }

    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }





#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Find/" + nameof(GameManager))]
    public static new void CreateAndShow()
    {
        ScriptableSingleton<GameManager>.CreateAndShow();
    }
#endif
}