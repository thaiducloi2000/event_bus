using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventBus
{
    public abstract class MonoBehaviourEventListener : MonoBehaviour
    {
        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();

        protected virtual void OnEnable() => RegisterEvents();
        protected virtual void OnDisable() => UnregisterEvents();
    }
}
