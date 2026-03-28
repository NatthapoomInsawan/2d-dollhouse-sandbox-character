using UnityEngine;
using System.Collections.Generic;
using DollhouseCharacter.Interfaces;

namespace DollhouseCharacter.Events
{
    public abstract class BaseGameEvent<T>: ScriptableObject
    {
        private readonly List<IEventListener<T>> listeners = new();

        public void RegisterListener(IEventListener<T> listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(IEventListener<T> listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

        public void Raise(T value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(value);
        }
    }
}
