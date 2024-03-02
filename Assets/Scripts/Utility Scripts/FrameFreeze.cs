using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameFreeze : MonoBehaviour
{
    [SerializeField]
    NormalCurve freezeCurve;


    public void Freeze(float duration)
    {
        StartCoroutine(FreezeFrame(duration));
    }

    private IEnumerator FreezeFrame(float duration)
    {
        float freezeTimer = 0;
        float normal = 0;
        float previousTimeScale = GlobalSettings.Current.timeScale;
        
        while (normal < 1f)
        {
            yield return null;
            freezeTimer += Time.unscaledDeltaTime;
            normal = freezeTimer / duration;

            Time.timeScale = previousTimeScale * (1f - freezeCurve.Evaluate(normal));
        }
    }
}
