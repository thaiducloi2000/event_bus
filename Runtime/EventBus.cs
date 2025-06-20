using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventBus
{
    public class EventBus<T> where T : IEventBus 
    {
        protected static readonly Dictionary<int, Delegate> listeners = new(); 
        public static void AddListener<T>(int eventID, EventCallback<T> callback)
        {
            if (listeners.ContainsKey(eventID))
            {
                listeners[eventID] = Delegate.Combine(listeners[eventID], callback);
            }
            else
            {
                listeners.Add(eventID, callback);
            }
        }

        public static void ClearAllListener()
        {
            listeners.Clear();
        }

        public static void PostEvent<T>(int eventID, T param = default)
        {
            if (!listeners.TryGetValue(eventID, out Delegate del)) return;

            var invocationList = del.GetInvocationList(); // Snapshot 1 lần
            for (int i = 0; i < invocationList.Length; i++)
            {
                var callback = invocationList[i] as EventCallback<T>;
                try
                {
                    callback?.Invoke(param);
                }
                catch (Exception ex)
                {
                    var method = callback?.Method;
                }
            }
        }

        public static void RemoveAllListener(int eventID)
        {
            if (listeners.ContainsKey(eventID))
                listeners[eventID] = null;
        }

        public static void RemoveListener<T>(int eventID, EventCallback<T> callback) 
        {
            if (listeners.ContainsKey(eventID))
            {
                listeners[eventID] = Delegate.Remove(listeners[eventID], callback);
                if (listeners[eventID] == null)
                {
                    listeners.Remove(eventID);
                }
            }
        } 
    }
}
