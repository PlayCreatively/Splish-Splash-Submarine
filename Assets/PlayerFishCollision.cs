using System.Collections;
using UnityEngine;

public class PlayerFishCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GlobalSettings.Current.player.recoveringFromCollision == false
            && collision.gameObject.CompareTag("Enemy"))
        {
            // If the player collides with an enemy, the player goes through hit routine
            StartCoroutine(HitRoutine());
        }
    }

    IEnumerator HitRoutine()
    {
        GlobalSettings.Current.player.recoveringFromCollision = true;

        Timer recoveryTimer = new(GlobalSettings.Current.player.collisionRecoverTime);

        while (!recoveryTimer)
        {
            GlobalSettings.Current.player.curVerticalSpeed = (1f - recoveryTimer) * GlobalSettings.Current.player.verticalSpeed;
            yield return null;
        }

        GlobalSettings.Current.player.recoveringFromCollision = false;
    }
}
