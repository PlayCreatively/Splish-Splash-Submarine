using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Setting Objects/GameSettings", order = 0)]
public class GameSettings : ScriptableObject
{
    public RadarSettings radar;
    public ShootingSettings shooting;
    public static GameSettings Current => current;
    static GameSettings current;
    private void Awake()
    {
        current = this;
    }
}