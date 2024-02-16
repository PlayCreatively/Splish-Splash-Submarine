using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFishCollision : MonoBehaviour
{
    public UnityEvent OnCollision;

    void Awake()
    {
        GlobalSettings.Current.player.recoveringFromCollision = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player is not recovering from a collision
        // and the player collides with something (presumed to be enemy... can also be EfB, doesn't matter)
        if (GlobalSettings.Current.player.recoveringFromCollision == false)
        {
            StartCoroutine(HitRoutine());
        }
    }

    IEnumerator HitRoutine()
    {
        OnCollision?.Invoke();

        GlobalSettings.Current.player.recoveringFromCollision = true;

        Timer recoveryTimer = new(GlobalSettings.Current.player.collisionRecoverTime);

        while (!recoveryTimer)
        {
            yield return null;
            GlobalSettings.Current.player.curVerticalSpeed = recoveryTimer.ClampedNormal() * GlobalSettings.Current.player.verticalSpeed;
        }

        GlobalSettings.Current.player.recoveringFromCollision = false;
    }
}
