using System.Collections.Generic;
using System;

namespace EventBus
{
    public class UiEventListener<TChannel> where TChannel : IEventChannel 
    {
        private readonly Dictionary<int, Delegate> _handlers = new();

        public void AddListener<TPayload>(int eventID, EventCallback<TPayload> callback) where TPayload : IEventUIData
        {
            EventBus<TChannel>.AddListener<TPayload>(eventID, callback);
            _handlers[eventID] = callback;
        }

        public void RemoveListener<TPayload>(int eventID, EventCallback<TPayload> callback) where TPayload : IEventUIData
        {
            EventBus<TChannel>.RemoveListener<TPayload>(eventID, callback);
            _handlers.Remove(eventID);
        }

        public void RemoveAllListeners()
        {
            foreach (var kv in _handlers)
            {
                EventBus<TChannel>.RemoveAllListener(kv.Key);
            }

            _handlers.Clear();
        }
    }

    public interface IEventUIData
    {
    }
}
