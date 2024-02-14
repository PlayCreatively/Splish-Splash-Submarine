using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(EnemyFromBehindSettings), menuName = Path + nameof(EnemyFromBehindSettings), order = 0)]
public class EnemyFromBehindSettings : SettingsBase<EnemyFromBehindSettings>
{
    [Min(0)]
    public float
        constMoveSpeed = 1,
        swipeSpeed = 1;
    [Min(10), Tooltip("At what distance from the enemy do you start to see the hand.")]
    public float tipPosition = 10;
        

    [Min(1)]
    public float dashingAffectiveness = 1;

    [HideInInspector]
    public float curDistanceFromPlayer = 1f;
}
