using System.Collections;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    Vector3 defaultPos;
    public float distance = 1f;
    public float speed = 10f;
    float seed;

    bool isRoutineRunning = false;

    void Start()
    {
        defaultPos = transform.localPosition;
        seed = Random.value * 100f;
    }

    void Update()
    {
        if (!isRoutineRunning)
            DoShake(defaultPos, speed, distance);
    }

    void DoShake(Vector3 origin, float speed, float distance)
    {
        transform.localPosition = origin + new Vector3(Mathf.PerlinNoise1D(seed + Time.time * speed) * 2f - 1f, Mathf.PerlinNoise1D(seed + Time.time * speed + Mathf.PI) * 2f - 1f, 0) * distance;
    }

    public void StartShortShakeRoutine() => StartShakeRoutine(.2f, 25);
    public void StartBigShakeRoutine() => StartShakeRoutine(.8f, 40);
    public void StartShakeRoutine(float duration, float intensity)
    {
        isRoutineRunning = true;
        StartCoroutine(MyShakeRoutine(duration, intensity));
        isRoutineRunning = false;
    }


    public IEnumerator MyShakeRoutine(float duration, float intensity)
    {
        isRoutineRunning = true;
        yield return this.ShakeRoutine(duration, intensity);
        isRoutineRunning = false;
    }

    public IEnumerator MyShakeRoutine(float duration, float speed, float distance)
    {
        isRoutineRunning = true;
        yield return this.ShakeRoutine(duration, speed, distance);
        isRoutineRunning = false;
    }
}

public static class ShakeHelper
{
    public static void StartShakeRoutine(this MonoBehaviour mono, float time, float intensity)
    {
        mono.StartCoroutine(mono.ShakeRoutine(time, intensity));
    }

    public static IEnumerator ShakeRoutine(this MonoBehaviour mono, float duration, float intensity)
    {
        yield return mono.ShakeRoutine(duration, intensity, intensity * .1f);
    }
    public static IEnumerator ShakeRoutine(this MonoBehaviour mono, float duration, float speed, float distance)
    {
        Vector3 origin = mono.transform.localPosition;
        float seed = Random.value * 100f;

        while (duration > 0)
        {
            mono.transform.localPosition = origin + new Vector3(Mathf.PerlinNoise1D(seed + Time.unscaledTime * speed * duration) * 2f - 1f, Mathf.PerlinNoise1D(seed + Time.unscaledTime * speed * duration + Mathf.PI) * 2f - 1f, 0) * (distance * duration);
            duration -= Time.unscaledDeltaTime;
            yield return null;
        }

        mono.transform.localPosition = origin;
    }



    public static void MoveToTargetRoutine(this MonoBehaviour mono, Vector3 target, float duration)
    {
        mono.StartCoroutine(MoveToTarget(mono, target, duration));
    }

    public static IEnumerator MoveToTarget(this MonoBehaviour mono, Vector3 target, float duration)
    {
        Vector3 moveDelta = (target - mono.transform.position) / duration;

        while (duration > 0)
        {
            mono.transform.position += moveDelta * Time.deltaTime;
            duration -= Time.deltaTime;
            yield return null;
        }
    }

}
 