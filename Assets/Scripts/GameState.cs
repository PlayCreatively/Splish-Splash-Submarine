using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Get => _get;
    static GameState _get;

    // Game State Variables //
    [HideInInspector]
    public int level = 0;
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
        //Camera.main.rect = new Rect(0, 0, .8f, .8f);


        switch (this.scene)
        {
            //case SceneType.MainMenu:
            //    break;
            case SceneType.Game:
                if (level == 0)
                {
                    Tutorial tutorial = Resources.Load<Tutorial>("Settings/LevelSettings/TutorialPrefab");
                    Instantiate(tutorial);
                } 
                GlobalSettings.Current.level = Resources.Load<LevelAsset>("Settings/LevelSettings/Level " + level);
                break;
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