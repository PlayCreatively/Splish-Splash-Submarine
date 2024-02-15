using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Timer dashCooldown;
    float dashSpeed;

    void Awake()
    {
        dashCooldown.Start(GlobalSettings.Current.player.dashCooldown);
        dashSpeed = GlobalSettings.Current.player.dashSpeed;
    }
    void Update()
    {
        if (dashCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
                dashCooldown.Restart();
            }
        }
    }
    void Dash()
    {
        GlobalSettings.Current.player.curVerticalSpeed = 3; 
    }
}
