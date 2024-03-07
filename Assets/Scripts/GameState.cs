using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Get => _get;
    public static GameState _get;



    // Game State Variables //
    [HideInInspector]
    public int level = 1;
    /// <summary>
    /// Normalized level progress
    /// </summary>
    public float LevelProgress => distanceTraveled / GlobalSettings.Current.LevelLength;
    [HideInInspector]
    public float distanceTraveled = 0;
    [HideInInspector]
    public SceneType scene = SceneType.StartMenu;
    [HideInInspector]
    public float playerVerticalSpeed;
    [HideInInspector]
    public int latchedEnemyCount = 0;
    [HideInInspector]
    public float efbMoveSpeedOverPlayer = 0, 
                 efbDistanceFromPlayer  = 1;



    void Awake()
    {
        if (_get == null || _get == this)
        {
            _get = this;
            Init();
        }
        else
            Destroy(gameObject);
    }

    void Init()
    {
        DontDestroyOnLoad(this);

        Time.timeScale = GlobalSettings.Current.timeScale;

        SceneManager.sceneLoaded += OnSceneLoad;
    }
    
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        this.scene = (SceneType)scene.buildIndex;

        switch (this.scene)
        {
            //case SceneType.MainMenu:
            //    break;
            //case SceneType.Game:
            //    break;
            case SceneType.Comic:
                FindAnyObjectByType<ComicManager>().comic = ComicAsset.Load(level);
                break;
        }
    }
}

[SerializeField]
public enum SceneType
{
    StartMenu,
    Game,
    Comic,
}