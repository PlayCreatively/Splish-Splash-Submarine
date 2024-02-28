using UnityEngine;

public class HUDLights : MonoBehaviour
{
    public void SetLights(bool on)
    {
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = on ? Color.white : new(.4f, .4f, .4f);
        }
    }
}
