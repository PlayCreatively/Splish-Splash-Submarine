using UnityEngine;
using System.Collections;

public class BlipTrail : MonoBehaviour
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

        OnBlip();
    }

    public void OnBlip()
    {
        i = (i + 1) % 2;
        StartCoroutine(StartTrail(BlipTails[i]));
    }

    IEnumerator StartTrail(TrailRenderer trailRend)
    {
        trailRend.emitting = false;
        trailRend.sortingLayerName = "Default";
        trailRend.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        Timer timer = new(GlobalSettings.Current.radar.scanSpeed);
        Color color = trailRend.startColor;

        while (!timer)
        {
            color.a = timer.SubNormal(.075f) - timer.Normal;
            trailRend.startColor = color;
            yield return null;
        }

        color.a = 1;
        trailRend.startColor = color;

        trailRend.Clear();
        trailRend.emitting = true;
        trailRend.sortingLayerName = "HiddenRadar";
        trailRend.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }
}
