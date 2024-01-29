using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipTag : MonoBehaviour
{
    [HideInInspector]
    public bool hasPingedThisScan;

    void OnEnable() => RadarManager.blipsTags.Add(this);

    void OnDisable() => RadarManager.blipsTags.Remove(this);

    public void Ping()
    {
        hasPingedThisScan = true;
    }

    IEnumerator PingRoutine()
    {
        yield return new WaitForSeconds(GlobalSettings.Current.radar.scanSpeed*.95f);
    }
}
