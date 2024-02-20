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
        }
    }

    public void OnBlip()
    {
        i = (i + 1) % 2;
        StartCoroutine(StartTrail());
    }

    IEnumerator StartTrail()
    {
        Timer timer = new(GlobalSettings.Current.radar.scanSpeed);
        while (!timer)
        {
            yield return null;
            BlipTails[i].startColor = new Color(BlipTails[i].startColor.r, BlipTails[i].startColor.g, BlipTails[i].startColor.b, 1 - timer);
        }
        BlipTails[i].GetComponent<TrailRenderer>().Clear();
    }
}
