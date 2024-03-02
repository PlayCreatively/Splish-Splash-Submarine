using System.Collections;
using UnityEngine;

/// <summary>
/// Runs a coroutine that minimizes the object's scale over time, then destroys it.
/// </summary>
public class Destroyer : MonoBehaviour
{
    public void Destroy()
    {
        StartCoroutine(DestroyRoutine(1));
    }
    public void Destroy(int milliseconds)
    {
        StartCoroutine(DestroyRoutine(milliseconds));
    }

    IEnumerator DestroyRoutine(int milliseconds)
    {
        Timer destroyTimer = new(milliseconds * .001f);
        Vector3 originalScale = transform.localScale;

        while (!destroyTimer)
        {
            yield return null;
            transform.localScale = originalScale * (1f-destroyTimer);
        }

        Destroy(gameObject);
    }
    
}
