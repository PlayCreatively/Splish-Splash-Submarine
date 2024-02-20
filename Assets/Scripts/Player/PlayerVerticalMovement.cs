using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    void Awake()
    {
        GlobalSettings.Current.player.curVerticalSpeed = GlobalSettings.Current.player.verticalSpeed;
    }

    void Update()
    {
        GlobalSettings.Current.enemyFromBehind.curDistanceFromPlayer += GlobalSettings.Current.player.curVerticalSpeed * Time.deltaTime;
    }
}
