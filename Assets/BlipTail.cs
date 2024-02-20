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
            BlipTails[i].time = GlobalSettings.Current.radar.scanSpeed;
            BlipTails[i].emitting = false;

        }
    }

    public void OnBlip()
    {
        i = (i + 1) % 2;
        StartCoroutine(StartTrail());
    }

    IEnumerator StartTrail()
    {
        BlipTails[i].emitting = false;
        Timer timer = new(GlobalSettings.Current.radar.scanSpeed);
        while (!timer)
        {
            yield return null;
            Color color = BlipTails[i].startColor;
            color.a = 1 - timer;
            BlipTails[i].startColor = BlipTails[i].endColor = color;
        }
        BlipTails[i].Clear();
        BlipTails[i].emitting = true;
    }
}
