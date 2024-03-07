using UnityEngine;

[CreateAssetMenu(fileName = "New"+nameof(ModeSettings), menuName = "Setting Objects/" + nameof(ModeSettings), order = -10)]
public class ModeSettings : ScriptableObject
{
    [Range(0, 1)]
    public float timeScale = 1;
    [SerializeField, Min(0)]
    float levelLengthInMinutes = 1;
    [HideInInspector]
    public float LevelLength;
    public PlayerSettings player;
    //public EnemySettings enemy;
    public EnemyFromBehindSettings enemyFromBehind;
    public RadarSettings radar;
    public ShootingSettings shooting;
    public SpawnerSettings spawnerSettings;

    private void OnEnable()
    {
        LevelLength = levelLengthInMinutes * 60f * player.verticalSpeed * Time.timeScale;
    }
}