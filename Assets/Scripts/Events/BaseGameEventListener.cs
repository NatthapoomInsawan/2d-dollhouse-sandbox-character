using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using DollhouseCharacter.Interfaces;

namespace DollhouseCharacter.Events
{
    public abstract class BaseGameEventListener<T> : MonoBehaviour, IEventListener<T>
    {
        [Serializable]
        public struct EventBinding<TValue> 
        {
            public BaseGameEvent<TValue> gameEvent;
            public UnityEvent<TValue> response;
        }

        [SerializeField] private List<EventBinding<T>> eventBindings = new List<EventBinding<T>>();

        private void OnEnable() => eventBindings.ForEach(binding => binding.gameEvent?.RegisterListener(this));
        private void OnDisable() => eventBindings.ForEach(binding => binding.gameEvent?.UnregisterListener(this));
        public void OnEventRaised(T value)
        {
            eventBindings.ForEach(binding =>
            {
                binding.response?.Invoke(value);
            });
        }
    }
}
