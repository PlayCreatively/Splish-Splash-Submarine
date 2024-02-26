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
        // turn off fish logic
        GetComponentInChildren<EnemyConstantMover>().enabled = false;
        GetComponentInChildren<MovePattern>().enabled = false;

        transform.parent = player;
        transform.localPosition = Vector3.down;
        transform.GetChild(0).localPosition = Vector3.zero;

        // try this
        enabled = false;

        ApplyLatchingSpeedChanges(latchingSpeedChange);
        Timer latchTimer = new(latchDuration);

        onLatch?.Invoke();

        while (!latchTimer)
            yield return null;

        onRelease?.Invoke();

        ApplyLatchingSpeedChanges(-latchingSpeedChange);
        Destroy(gameObject);
    }

    void ApplyLatchingSpeedChanges(float latchingSpeedChange)
    {
        //GlobalSettings.Current.player.curVerticalSpeed -= latchingSpeedChange;
        GlobalSettings.Current.enemyFromBehind.curMoveSpeedOverPlayer += latchingSpeedChange;
    }
}
