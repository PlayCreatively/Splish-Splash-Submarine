using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(EnemyFromBehindSettings), menuName = Path + nameof(EnemyFromBehindSettings), order = 0)]
public class EnemyFromBehindSettings : SettingsBase<EnemyFromBehindSettings>
{
    public float maxDistanceFromPlayer = 5;
    [Tooltip("The ratio of the enemy speed when detracting back from the player.")]
    public float detractingSpeedRatio = .5f;
}
