using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Latches onto the player when below the player's position
/// </summary>
public class PlayerLatcher : MonoBehaviour
{
    public float latchingSpeedChange = .2f;
    public float latchDuration = 2f;

    [SerializeField]
    UnityEvent onLatch, onRelease;

    Transform player;

    void Start()
    {
        player = GlobalSettings.Current.player.Ref.transform;
    }

    void Update()
    {
        if (transform.position.y - 2 < player.position.y)
            StartCoroutine(LatchOntoPlayerRoutine());
    }

    IEnumerator LatchOntoPlayerRoutine()
    {
        Destroy(transform.parent.gameObject);
        transform.parent = null;
        enabled = false;

        yield return StartCoroutine(InterpolateRoutine(player.position, 12f));

        transform.parent = player.GetChild(0).GetChild(0);
        transform.localPosition = Vector3.down;

        ApplyLatchingSpeedChanges(latchingSpeedChange);
        Timer latchTimer = new(latchDuration);

        GameState.Get.latchedEnemyCount++;
        onLatch?.Invoke();

        while (!latchTimer)
            yield return null;

        GameState.Get.latchedEnemyCount--;
        onRelease?.Invoke();

        ApplyLatchingSpeedChanges(-latchingSpeedChange);
        Destroy(gameObject);
    }

    IEnumerator InterpolateRoutine(Vector3 target, float speed)
    {
        Vector3 startPos = transform.position; 
        float duration = Vector3.Distance(startPos, target) / speed;
        Timer moveTimer = new(duration);
        while(!moveTimer)
        {
            yield return null;
            transform.position = Vector3.Lerp(startPos, target, moveTimer);
        }
    }

    void ApplyLatchingSpeedChanges(float latchingSpeedChange)
    {
        //GameState.Get.playerVerticalSpeed -= latchingSpeedChange;
        GameState.Get.efbMoveSpeedOverPlayer += latchingSpeedChange;
    }
}
