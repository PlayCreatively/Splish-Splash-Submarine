using UnityEngine;

[CreateAssetMenu(fileName = "RadarSettings", menuName = Path + nameof(RadarSettings), order = 0)]
public class RadarSettings : SettingsBase<RadarSettings>
{
    [Tooltip("How long it takes to do a scan."), Min(.01f)]
    public float scanSpeed = 0.5f;
    [Tooltip("How long the radar ping lasts."), Min(.2f)]
    public float pingDuration = 0.5f;
}