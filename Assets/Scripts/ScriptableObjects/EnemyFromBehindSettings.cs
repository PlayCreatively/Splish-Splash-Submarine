using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(EnemyFromBehindSettings), menuName = Path + nameof(EnemyFromBehindSettings), order = 0)]
public class EnemyFromBehindSettings : SettingsBase<EnemyFromBehindSettings>
{
    [HideInInspector]
    public float curMoveSpeedOverPlayer = 0;
    [Min(0)]
    public float swipeDuration = 1;

    [HideInInspector]
    public float curDistanceFromPlayer = 1;
    public float maxDistanceFromPlayer = 5;
}
