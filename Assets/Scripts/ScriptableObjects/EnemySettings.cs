using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(EnemySettings), menuName = Path + nameof(EnemySettings), order = 0)]
public class EnemySettings : SettingsBase<EnemySettings>
{
    [Min(0)]
    public float fallSpeed = 1;

    [HideInInspector]
    public float curFallspeed;
}