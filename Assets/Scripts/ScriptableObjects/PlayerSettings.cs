using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(PlayerSettings), menuName = Path + nameof(PlayerSettings), order = 0)]
public class PlayerSettings : SettingsBase<PlayerSettings>
{
    [Min(0)]
    public float boostSpeed = 1;

    [Min(0)]
    public float horizontalSpeed = 1;

    public  Transform Ref => _player;
    private Transform _player;

    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }
}
