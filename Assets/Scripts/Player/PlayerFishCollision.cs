using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFishCollision : MonoBehaviour
{
    public UnityEvent OnCollision;

    PlayerSettings Settings => GlobalSettings.Current.player;

    void Awake()
    {
        Settings.recoveringFromCollision = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player is not recovering from a collision
        // and the player collides with something (presumed to be enemy... can also be EfB, doesn't matter)
        if (Settings.recoveringFromCollision == false)
        {
            StartCoroutine(HitRoutine());
        }
    }

    IEnumerator HitRoutine()
    {
        OnCollision?.Invoke();

        Settings.recoveringFromCollision = true;

        Timer recoveryTimer = new(Settings.collisionRecoverTime);

        while (!recoveryTimer)
        {
            yield return null;
            Settings.curVerticalSpeed = Settings.collisionRecoverCurve.Evaluate(recoveryTimer.ClampedNormal()) * Settings.verticalSpeed;
        }

        Settings.recoveringFromCollision = false;
    }
}
