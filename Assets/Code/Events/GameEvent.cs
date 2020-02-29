using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject, IRaisable, IListenable
{
    private readonly List<IListener> _listeners = new List<IListener>();


    public void Raise()
    {
        // counting down to avoid list removal errors
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised();
    }


    public void RegisterListener(IListener listener)
    {
        if (!_listeners.Contains(listener))
            _listeners.Add(listener);
    }

    public void UnregisterListener(IListener listener)
    {
        if (_listeners.Contains(listener))
            _listeners.Remove(listener);
    }
}