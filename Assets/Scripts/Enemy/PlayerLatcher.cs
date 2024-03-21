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
        GameState.Get.OnLevelComplete += Destroy;
    }

    void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnDisable()
    {
        GameState.Get.OnLevelComplete -= Destroy;
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
        GetComponentInChildren<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        yield return StartCoroutine(InterpolateRoutine(player.position, 12f));

        transform.parent = player.GetChild(0).GetChild(0);

        ApplyLatchingSpeedChanges();
        Timer latchTimer = new(latchDuration);

        GameState.Get.LatchedEnemyCount++;
        onLatch?.Invoke();

        // Show latch tutorial comic if this is the first latch
        if(GameState.Get.hasLatched == false)
        {
            GameState.Get.hasLatched = true;
            ComicManager.InstantiateComicModal(0);
        }

        while (!latchTimer)
            yield return null;

        GameState.Get.LatchedEnemyCount--;
        onRelease?.Invoke();

        ApplyLatchingSpeedChanges();
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

    void ApplyLatchingSpeedChanges()
    {
        GameState.Get.PlayerVerticalSpeed = Mathf.Lerp(GlobalSettings.Current.player.verticalSpeed, GlobalSettings.Current.player.verticalSpeed / (GameState.Get.LatchedEnemyCount + 1), .5f);
    }
}
