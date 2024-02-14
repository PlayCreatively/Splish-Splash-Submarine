using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(PlayerSettings), menuName = Path + nameof(PlayerSettings), order = 0)]
public class PlayerSettings : SettingsBase<PlayerSettings>
{
    [Min(0), Header("Boost")]
    public float boostSpeed = 1;

    [Min(0), Header("HorizontalMovement")]
    public float horizontalSpeed = 1;
}
