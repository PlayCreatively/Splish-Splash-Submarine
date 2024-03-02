using UnityEngine;
using UnityEngine.Events;

public class ScriptableEventListenerFloat: MonoBehaviour
{
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
        Response.Invoke(value);
    }
}