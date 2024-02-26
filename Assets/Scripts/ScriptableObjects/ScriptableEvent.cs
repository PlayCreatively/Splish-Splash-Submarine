using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewEvent", menuName = "Event")]
public class ScriptableEvent : ScriptableObject
{
    [SerializeField]
    UnityEvent StaticResponse;

    readonly List<ScriptableEventListener> listeners = new();

    public void Raise()
    {
        StaticResponse?.Invoke();
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(ScriptableEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(ScriptableEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
