using UnityEngine;

[CreateAssetMenu(fileName = "New" + nameof(ShootingSettings), menuName = Path + nameof(ShootingSettings), order = 0)]
public class ShootingSettings : SettingsBase<ShootingSettings>
{
    [Min(.1f)]
    public float reloadTime = 6f;
    [Min(1)]
    public float bulletSpeed = 1f;
    //[Header("not implemented yet...")]
    //public float maxAimAngle = 25;
    //[Header("not implemented yet...")]
    //public float rotationTime = 1f;

}
