using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(PlayerSettings), menuName = Path + nameof(PlayerSettings), order = 0)]
public class PlayerSettings : SettingsBase<PlayerSettings>
{
    [Min(0)]
    public float verticalSpeed = 1;

    public NormalCurve horizontalCurve;
    [Min(0)] 
    public float horizontalSpeed = 1;

    [HideInInspector]
    public float curVerticalSpeed;

    public  Transform Ref => _player != null 
        ? _player 
        : _player = GameObject.FindWithTag("Player").transform;
    private Transform _player;  
}
