using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShootingSettings", menuName = Path + nameof(ShootingSettings), order = 0)]
public class ShootingSettings : SettingsBase<ShootingSettings>
{
    [Min(.1f)]
    public float shootRate = 0.5f;
    [Min(1)]
    public float bulletSpeed = 1f;
}
