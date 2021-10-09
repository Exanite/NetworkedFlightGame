using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source
{
    public class EventBus : MonoBehaviour
    {
        public bool shouldLogEvents;
        
        private Dictionary<Type, List<object>> listenerLists;

        private void Awake()
        {
            listenerLists = new Dictionary<Type, List<object>>();
        }

        public void AddListener<T>(IEventListener<T> listener) where T : Event
        {
            var type = typeof(T);

            if (!listenerLists.ContainsKey(typeof(T)))
            {
                listenerLists.Add(type, new List<object>());
            }

            listenerLists[type].Add(listener);
        }

        public void RemoveListener<T>(IEventListener<T> listener) where T : Event
        {
            if (!listenerLists.TryGetValue(typeof(T), out var listenerList))
            {
                return;
            }

            listenerList.Remove(listener);
        }

        public void PushEvent<T>(T e) where T : Event
        {
            if (shouldLogEvents)
            {
                Debug.Log(e);
            }
            
            var type = typeof(T);

            if (!listenerLists.TryGetValue(type, out var listenerList))
            {
                return;
            }

            foreach (var listener in listenerList)
            {
                ((IEventListener<T>)listener).On(e);
            }
        }
    }

    public interface IEventListener<in T> where T : Event
    {
        void On(T e);
    }

    public abstract class Event { }
}