using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameState : MonoBehaviour
{
    public static GameState Get => _get;
    static GameState _get;

    // Game State Variables //
    [HideInInspector]
    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            GlobalSettings.Current.level = Resources.Load<LevelAsset>("Settings/LevelSettings/Level " + value);
        }
    }
    int _level = 0;
    /// <summary>
    /// Normalized level progress
    /// </summary>
    public float LevelProgress => distanceTraveled / GlobalSettings.Current.level.LevelLength;
    [HideInInspector]
    public float distanceTraveled = 0;
    [HideInInspector]
    public SceneType scene = SceneType.StartMenu;
    [HideInInspector]
    public float playerVerticalSpeed;
    [HideInInspector]
    public int latchedEnemyCount = 0;
    [HideInInspector]
    public float efbDistanceFromPlayer  = 1;

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
        GameManager.FadeIn();

        switch (this.scene)
        {
            case SceneType.StartMenu:
                Level = 0;
                break;
            case SceneType.Game:
                if (Level == 0)
                {
                    Tutorial tutorial = Resources.Load<Tutorial>("Settings/LevelSettings/TutorialPrefab");
                    Instantiate(tutorial);
                } 
                break;
            case SceneType.Comic:
                FindAnyObjectByType<ComicManager>().comic = ComicAsset.Load(Level);
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