using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadarManager : MonoBehaviour
{
    public static readonly List<BlipTag> blipsTags = new();

    [SerializeField]
    SpriteRenderer blipPrefab;

    public UnityEvent OnBlip;
    Pool<SpriteRenderer> pool;
    Timer scanTimer;
    Transform poolParent;
    

    void Awake()
    {
        GlobalSettings.Current.radar.onValidate += 
            settings => scanTimer = new(settings.scanSpeed);

        poolParent = new GameObject("Blip Pool").transform;

        pool = new(() => Instantiate(blipPrefab, poolParent), (isSpawning, blip) => blip.gameObject.SetActive(isSpawning));
        scanTimer = new(GlobalSettings.Current.radar.scanSpeed);

    }

    void Update()
    {
        if(!scanTimer)
        {
            foreach (BlipTag blipTag in blipsTags)
                if (!blipTag.hasPingedThisScan
                    && CalculatePos(blipTag.transform.position) < scanTimer)
                {
                    blipTag.hasPingedThisScan = true;
                    blipTag.GetComponentInParent<BlipTrail>().OnBlip();
                    OnBlip?.Invoke();
                    //StartCoroutine(BlipRoutine(blipTag.transform.position));
                }

            float angle = 180 + -(scanTimer * 180);
            transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            foreach (BlipTag blipTag in blipsTags)
                blipTag.hasPingedThisScan = false;

            scanTimer.Offset(GlobalSettings.Current.radar.scanSpeed);
        }

    }

    // Tells the angle of the object relative to the radar, from 0 to 1.
    public float CalculatePos(Vector3 pos)
    {
        return Vector2.Angle(Vector2.left, -transform.position + pos) / 180f;
    }

    public IEnumerator BlipRoutine(Vector3 position)
    {
        SpriteRenderer blipVisual = pool.Borrow();
        blipVisual.transform.position = position;
        Timer timer = new(GlobalSettings.Current.radar.scanSpeed * GlobalSettings.Current.radar.pingDuration);
        while (!timer)
        {
            var color = blipVisual.color;
            color.a = 1 - timer;
            blipVisual.color = color;

            yield return null;
        }
        pool.Return(blipVisual);
    }
}
