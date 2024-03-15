using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 basePosition;

    void Start()
    {
        basePosition = transform.localPosition;
        GameState.Get.OnPlayerLatchChange += OnPlayerLatchChange;
    }

    private void OnDisable()
    {
        GameState.Get.OnPlayerLatchChange -= OnPlayerLatchChange;
    }

    void OnPlayerLatchChange(int latchedEnemyCount)
    {
        StopAllCoroutines();
        StartCoroutine(InterpolateRoutine(new Vector3(0, -1f * (1f-(1f / (latchedEnemyCount+1)))) + basePosition, .4f));
    }

    IEnumerator InterpolateRoutine(Vector3 targetPosition, float speed)
    {
        Timer timer = new Timer(speed);
        Vector3 startPos = transform.localPosition;
        while (!timer)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPosition, timer);
            yield return null;
        }
    }
}
