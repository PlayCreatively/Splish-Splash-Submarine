using UnityEngine;
using System.Collections;

public class BlipTrail : MonoBehaviour
{
    //public TrailRenderer blipTailPrefab;
    LocalTrailRenderer[] BlipTrails;

    int i = 0;

    void Awake()
    {
        //BlipTails = new LocalTrailRenderer[2];
        BlipTrails = GetComponents<LocalTrailRenderer>();
        for (int i = 0; i < 2; i++)
        {
            BlipTrails[i].emitting = false;
        }

        OnBlip();
    }

    public void OnBlip()
    {
        i = (i + 1) % 2;
        StartCoroutine(StartTrail(BlipTrails[i]));
    }

    IEnumerator StartTrail(LocalTrailRenderer trailRend)
    {
        trailRend.emitting = false;
        trailRend.trail.sortingLayerName = "Default";
        trailRend.trail.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        Timer timer = new(GlobalSettings.Current.radar.scanSpeed);
        Color color = trailRend.Color;

        while (!timer)
        {
            yield return null;
            color.a = timer.SubNormal(.075f) - timer.Normal;
            trailRend.Color = color;
        }

        //color.a = 1;
        //trailRend.Color = color;
        //trailRend.trail.sortingLayerName = "HiddenRadar";
        //trailRend.trail.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        trailRend.Clear();
        trailRend.emitting = true;
    }
}
