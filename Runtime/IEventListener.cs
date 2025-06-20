using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventBus
{
    public interface IEventListener
    {
        void SetupEventListener();
        void RemoveEventListener();
    }
}
