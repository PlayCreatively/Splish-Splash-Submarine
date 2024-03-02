using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewFloatEvent", menuName = "Events/Float Event")]
public class ScriptableEventFloat : ScriptableObject
{
    [SerializeField]
    UnityEvent<float> StaticResponse;

    readonly List<ScriptableEventListenerFloat> listeners = new();

    public void Raise(float value)
    {
        StaticResponse?.Invoke(value);
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(ScriptableEventListenerFloat listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(ScriptableEventListenerFloat listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}