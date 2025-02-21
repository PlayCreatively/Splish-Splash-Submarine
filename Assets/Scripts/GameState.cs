using System;
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
            var lvl = Resources.Load<LevelAsset>("Settings/LevelSettings/Level " + value);
            if(lvl != null)
                GlobalSettings.Current.level = lvl;
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

    public float PlayerVerticalSpeedUnscaled => _playerVerticalSpeed;
    public float PlayerVerticalSpeed
    {
        get => _playerVerticalSpeed * GlobalSettings.Current.level.moveSpeedMultiplier;
        set => _playerVerticalSpeed = value;
    }
    float _playerVerticalSpeed;
    [HideInInspector]
    public int LatchedEnemyCount
    {
        get => _latchedEnemyCount;
        set
        {
            _latchedEnemyCount = value;
            OnPlayerLatchChange?.Invoke(value);
        }
    }
    int _latchedEnemyCount = 0;
    [HideInInspector]
    public float efbDistanceFromPlayer  = 1;
    /// <summary>
    /// Has the player been latched onto before.
    /// </summary>
    [HideInInspector]
    public bool hasLatched = true;
    [HideInInspector]
    public Action<int> OnPlayerLatchChange;
    public Action OnLevelComplete;

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

        hasLatched = true;
        Time.timeScale = GlobalSettings.Current.timeScale;

        // Set the current level to the currently loaded level
        _level = int.Parse($"{GlobalSettings.Current.level.name[^1]}");

        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OpenCredits()
    {
        Transform canvas = FindAnyObjectByType<Canvas>().transform;
        canvas.Find("TitleScreen").gameObject.SetActive(false);
        canvas.Find("CreditScreen").gameObject.SetActive(true);
    }
    
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        this.scene = (SceneType)scene.buildIndex;
        GameManager.FadeIn();

        switch (this.scene)
        {
            case SceneType.StartMenu:
                // If the game has been completed, show the credits
                if(Level > 3)
                    OpenCredits();
                // Reset level
                Level = 0;
                break;
            case SceneType.Game:
                OnLevelComplete = null;

                if (Level == 0)
                {
                    hasLatched = false;
                    Tutorial tutorial = Resources.Load<Tutorial>("Settings/LevelSettings/TutorialPrefab");
                    Instantiate(tutorial);
                } 
                // Game completed
                else if(Level > 3)
                {
                    GameManager.LoadScene(SceneType.StartMenu);
                    break;
                }
                // Assign background according to level
                GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Backgrounds/Background " + Mathf.Clamp(Level, 1, 3));
                break;
            case SceneType.Comic:
                    FindAnyObjectByType<ComicManager>().comic = ComicAsset.Load(Level-1);
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