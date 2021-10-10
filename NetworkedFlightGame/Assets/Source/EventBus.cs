using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source
{
    public class EventBus : MonoBehaviour
    {
        [Header("Settings")]
        public string logPrefix;
        public bool shouldLogEvents;
        
        private Dictionary<Type, List<object>> listenerLists;

        private void Awake()
        {
            listenerLists = new Dictionary<Type, List<object>>();
        }

        public void AddListener<T>(IEventListener<T> listener)
        {
            var type = typeof(T);

            if (!listenerLists.ContainsKey(typeof(T)))
            {
                listenerLists.Add(type, new List<object>());
            }

            listenerLists[type].Add(listener);
        }

        public void RemoveListener<T>(IEventListener<T> listener)
        {
            if (!listenerLists.TryGetValue(typeof(T), out var listenerList))
            {
                return;
            }

            listenerList.Remove(listener);
        }

        public void PushEvent<T>(T e)
        {
            if (shouldLogEvents)
            {
                Debug.Log($"{logPrefix} {e}");
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

    public interface IEventListener<in T>
    {
        void On(T e);
    }
}