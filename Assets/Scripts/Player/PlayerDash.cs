using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Timer dashTimer;
    float dashSpeed;
    bool isDashing;

    void Awake()
    {
        GlobalSettings.Current.player.curVerticalSpeed = GlobalSettings.Current.player.verticalSpeed;
        dashTimer.Start(GlobalSettings.Current.player.dashDuration);
        dashSpeed = GlobalSettings.Current.player.dashSpeed;
    }

    void Update()
    {
        if (dashTimer)
        {
            isDashing = false;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                isDashing = true;
                dashTimer.Restart();
            }
        }

        if (isDashing)
        {
            // If collided whilst dashing 
            if(GlobalSettings.Current.player.recoveringFromCollision)
            {
                isDashing = false;
                dashTimer.Restart();
                return;
            }

            float curDashSpeed = dashSpeed * GlobalSettings.Current.player.dashCurve.Evaluate(dashTimer);
            GlobalSettings.Current.player.curVerticalSpeed = GlobalSettings.Current.player.verticalSpeed + curDashSpeed;
        }

        GlobalSettings.Current.enemyFromBehind.curDistanceFromPlayer += GlobalSettings.Current.player.curVerticalSpeed * Time.deltaTime;

    }
}
