using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Timer dashCooldown;

    void Awake()
    {
        dashCooldown.Start(GlobalSettings.Current.player.dashCooldown);
    }
    void Update()
    {
        if (dashCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                dashCooldown.Restart();
            }
        }
    }
}
