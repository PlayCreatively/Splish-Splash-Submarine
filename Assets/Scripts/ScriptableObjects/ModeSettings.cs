using UnityEngine;

[CreateAssetMenu(fileName = "New"+nameof(ModeSettings), menuName = "Setting Objects/" + nameof(ModeSettings), order = -10)]
public class ModeSettings : ScriptableObject
{
    [Range(0, 1)]
    public float timeScale = 1;
    public LevelAsset level;
    public PlayerSettings player;
    //public EnemySettings enemy;
    public EnemyFromBehindSettings enemyFromBehind;
    public RadarSettings radar;
    public ShootingSettings shooting;
    [Range(0, 1)]
    public float musicVolume = 1; 
}