using UnityEngine;
using UnityEngine.Events;

public class ScriptableEventListenerFloat: MonoBehaviour
{
    [Header("Remapping")]
    public float min = 0;
    public float max = 1;

    [Space(10)]
    public ScriptableEventFloat Event;
    public UnityEvent<float> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(float value)
    {
        Response.Invoke(Mathf.Lerp(min, max, value));
    }
}