using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

struct Timer
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

    public readonly bool Finished() => Time.time >= startTime + duration;

    public readonly float Normal()
    {
        return (Time.time - startTime) / duration;
    }

    public static implicit operator bool (Timer timer)
    {
        return timer.Finished();
    }

    public static implicit operator float (Timer timer)
    {
        return timer.Normal();
    }
}
