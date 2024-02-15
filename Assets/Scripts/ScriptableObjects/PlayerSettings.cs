using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(PlayerSettings), menuName = Path + nameof(PlayerSettings), order = 0)]
public class PlayerSettings : SettingsBase<PlayerSettings>
{
    [Min(0)]
    public float dashSpeed = 1;

    [Min(0)]
    public float dashDuration = 1;
    
    [Min(0), Tooltip("How long it takes the player to recover from colliding with a fish")]
    public float collisionRecoverTime = 1;

    [Min(0)] public float 
        horizontalSpeed = 1,
        verticalSpeed = 1;

    [HideInInspector]
    public float curVerticalSpeed;
    public bool recoveringFromCollision;

    public  Transform Ref => _player != null 
        ? _player 
        : _player = GameObject.FindWithTag("Player").transform;
    private Transform _player;  
}
