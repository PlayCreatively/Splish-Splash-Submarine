using UnityEngine;

[CreateAssetMenu(fileName = "New"+nameof(ModeSettings), menuName = "Setting Objects/" + nameof(ModeSettings), order = -10)]
public class ModeSettings : ScriptableObject
{
    public PlayerSettings player;
    //public EnemySettings enemy;
    public EnemyFromBehindSettings enemyFromBehind;
    public RadarSettings radar;
    public ShootingSettings shooting;
    public SpawnerSettings spawnerSettings;
}