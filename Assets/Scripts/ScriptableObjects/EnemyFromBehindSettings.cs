using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(EnemyFromBehindSettings), menuName = Path + nameof(EnemyFromBehindSettings), order = 0)]
public class EnemyFromBehindSettings : SettingsBase<EnemyFromBehindSettings>
{
    [SerializeField, Min(0), Tooltip("How much it moves over the player's base speed.")]
    float moveSpeedOverPlayer = 1;
    [HideInInspector]
    public float VerticalSpeed => moveSpeedOverPlayer + GlobalSettings.Current.player.verticalSpeed;
    [Min(0)]
    public float swipeDuration = 1;
    [Min(2), Tooltip("At what distance from the enemy do you start to see the hand.")]
    public float tipPosition = 2;

    //[Min(1)]
    //public float dashingAffectiveness = 1;

    [Min(0)]
    public float initialDistanceFromPlayer = 10;

    [HideInInspector]
    public float curDistanceFromPlayer = 1;

    void OnEnable()
    {
        curDistanceFromPlayer = initialDistanceFromPlayer;
    }
}
