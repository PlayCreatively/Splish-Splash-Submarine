using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Timer dashDuration;
    float dashSpeed;
    bool isDashing;

    void Awake()
    {
        GlobalSettings.Current.player.curVerticalSpeed = GlobalSettings.Current.player.verticalSpeed;
        dashDuration.Start(GlobalSettings.Current.player.dashDuration);
        dashSpeed = GlobalSettings.Current.player.dashSpeed;
    }

    void Update()
    {
        if (dashDuration)
        {
            if (isDashing) 
                Debug.Log("Stopped dashing");
            isDashing = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDashing = true;
                dashDuration.Restart();
                Debug.Log("Started dashing");
            }
        }

        if (isDashing)
        {
            float curDashSpeed = dashSpeed - dashSpeed * dashDuration;
            GlobalSettings.Current.player.curVerticalSpeed = GlobalSettings.Current.player.verticalSpeed + curDashSpeed;
            GlobalSettings.Current.enemyFromBehind.curDistanceFromPlayer += curDashSpeed * Time.deltaTime;
        }
    }
}
