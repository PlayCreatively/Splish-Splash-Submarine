using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Setting Objects/" + nameof(GameSettings), order = 0)]
public class GameSettings : ScriptableObject
{
    public PlayerSettings player;
    //public EnemySettings enemy;
    public EnemyFromBehindSettings enemyFromBehind;
    public RadarSettings radar;
    public ShootingSettings shooting;
    public SpawnerSettings spawnerSettings;
}