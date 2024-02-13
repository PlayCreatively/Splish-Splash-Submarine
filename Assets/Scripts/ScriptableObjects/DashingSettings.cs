using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(PlayerSettings), menuName = Path + nameof(PlayerSettings), order = 0)]
public class PlayerSettings : SettingsBase<PlayerSettings>
{
    [Min(0), Header("Boost")]
    public float boostSpeed = 1;
    public ADSREnvelope boostResponsiveness = ADSREnvelope.Default();

    [Min(0), Header("HorizontalMovement")]
    public float horizontalSpeed = 1;
    public ADSREnvelope horizontalResponsiveness = ADSREnvelope.Default();
}
