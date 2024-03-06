using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Get => _get;
    public static GameState _get;

    // Game State Variables //
    [HideInInspector]
    public float playerVerticalSpeed;
    [HideInInspector]
    public int latchedEnemyCount;
    [HideInInspector]
    public float efbMoveSpeedOverPlayer = 0, 
                 efbDistanceFromPlayer  = 1;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_get == null)
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
    }
}
