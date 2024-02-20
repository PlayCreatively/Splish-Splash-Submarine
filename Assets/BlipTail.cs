using UnityEngine;
using System.Collections;

public class BlipTail : MonoBehaviour
{
    public TrailRenderer blipTailPrefab;
    TrailRenderer[] BlipTails;

    int i = 0;

    void Awake()
    {
        BlipTails = new TrailRenderer[2];
        for (int i = 0; i < 2; i++)
        {
            BlipTails[i] = Instantiate(blipTailPrefab, transform.position, Quaternion.identity, transform);
            BlipTails[i].emitting = false;
        }
    }

    public void OnBlip()
    {
        i = (i + 1) % 2;
        StartCoroutine(StartTrail(BlipTails[i]));
    }

    IEnumerator StartTrail(TrailRenderer trailRend)
    {
        trailRend.emitting = false;
        Timer timer = new(GlobalSettings.Current.radar.scanSpeed);

        while (!timer)
        {
            yield return null;
            Color color = trailRend.startColor;
            color.a = 1f - timer.ClampedNormal();
            trailRend.startColor = trailRend.endColor = color;
        }

        trailRend.Clear();
        trailRend.emitting = true;
    }
}
