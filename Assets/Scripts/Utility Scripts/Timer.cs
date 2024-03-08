using UnityEngine;

public struct Timer
{
    float startTime, duration;

    public Timer(float duration)
    {
        this.duration = duration;
        startTime = Time.time;
    }

    public void Start(float duration)
    {
        this.duration = duration;
        startTime = Time.time;
    }

    public void Restart()
    {
        startTime = Time.time;
    }

    public void Offset(float duration)
    {
        startTime += duration;
    }

    public readonly float SubNormal(float max)
    {
        return Mathf.Clamp01(UnClampedNormal / max);
    }

    public readonly bool Finished => Time.time >= startTime + duration;

    public readonly float Normal => Mathf.Clamp01(UnClampedNormal);
    public readonly float Inverse => 1f - Normal;
    public readonly float UnClampedNormal => (Time.time - startTime) / duration;

    public static implicit operator bool (Timer timer)
    {
        return timer.Finished;
    }

    public static implicit operator float (Timer timer)
    {
        return timer.Normal;
    }
}
