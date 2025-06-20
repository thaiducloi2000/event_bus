# 🎯 Event Bus for Unity

**Package Name**: `com.littlekid.event_bus`  
**Display Name**: Event Bus  
**Version**: `0.0.2`  
**Unity Version**: `2022.3+`

A lightweight, strongly-typed and extensible Event Bus system for Unity. Designed to decouple gameplay systems with minimal effort while remaining modular and easy to integrate in existing projects.

---

## 📦 Installation

You can add this package via **Git URL** in Unity Package Manager:

1. Open Unity → `Window > Package Manager`
2. Click the `+` icon → `Add package from Git URL`
3. Paste:

```
https://github.com/thaiducloi2000/event_bus.git
```

Alternatively, you can add it directly in `manifest.json`:

```json
{
  "dependencies": {
    "com.littlekid.event_bus": "https://github.com/thaiducloi2000/event_bus.git"
  }
}
```

---

## 🧩 Features

- ✅ **Generic and strongly-typed** event system  
- ✅ **Minimal boilerplate**, easy to use  
- ✅ **No dependencies**, works with Unity out of the box  
- ✅ Interface-driven design for scalability  
- ✅ Supports **ScriptableObject**-based event buses  
- ✅ Utility extension methods for mass registration

---

## 🔧 Usage Guide

### 1. Define Your Event Data

All data types sent through the event system must implement `IBusdata<T>`:

```csharp
public struct ChessPositionChangeParam : IBusdata<ChessPositionChangeParam>
{
    public Dictionary<int, Chess> chessInBoard;
    public List<Position> positions;
}
```

### 2. Create a Listener

Implement `IEvenListener` for any class that listens to events:

```csharp
public class CoverManager : MonoBehaviour, IEvenListener
{
    private GameplayEvent Event;

    private void OnEnable()
    {
        Event = GameplayManager.GameEvent;
        SetupEventListener();
    }

    private void OnDisable()
    {
        RemoveEventListener();
    }

    public void SetupEventListener()
    {
        Event.AddListener<ChessPositionChangeParam>(
            (int)GameplayEventID.OnChessesPositionChange, OnPositionChanged);
    }

    public void RemoveEventListener()
    {
        Event.RemoveListener<ChessPositionChangeParam>(
            (int)GameplayEventID.OnChessesPositionChange, OnPositionChanged);
    }

    private void OnPositionChanged(ChessPositionChangeParam param)
    {
        // Handle logic here
    }
}
```

### 3. Dispatch an Event

Post events using the EventBus:

```csharp
var data = new ChessPositionChangeParam
{
    chessInBoard = someDictionary,
    positions = someList
};

GameplayManager.GameEvent.PostEvent((int)GameplayEventID.OnChessesPositionChange, data);
```

### 4. Register All Listeners Automatically (Optional)

If your listeners are children of a common GameObject:

```csharp
this.gameObject.RegisterAllListeners();
this.gameObject.UnregisterAllListeners();
```

Uses `EventBusHelper` for convenience and cleanup.

---

## ⚠️ Important Notes

### 🚫 Avoid Using Anonymous Lambdas as Listeners

While the system allows lambda expressions, **do not use them to register listeners**. This is because lambdas create new delegate instances that:

- ❌ Cannot be unregistered using `RemoveListener`
- ❌ Lead to memory leaks or duplicate callbacks
- ❌ Cause subtle bugs that are hard to trace

✅ **Correct usage**:

```csharp
void OnEventReceived(MyEventData data)
{
    // handle event
}

eventBus.AddListener<MyEventData>(MyEventID, OnEventReceived);
eventBus.RemoveListener<MyEventData>(MyEventID, OnEventReceived);
```

❌ **Avoid this**:

```csharp
eventBus.AddListener<MyEventData>(MyEventID, data => Debug.Log(data));
```

> Unity compares delegates by reference. Lambdas are unique instances that cannot be matched for removal.

---

## 🗂 Project Structure

```
event-bus/
├── Runtime/
│   ├── EventBus.cs
│   ├── GameplayEvent.cs
│   ├── IEventBus.cs
│   ├── IEvenListener.cs
│   ├── IBusdata.cs
│   └── EventBusHelper.cs
├── Editor/
├── package.json
└── README.md
```

---

## 🧪 Unit Testing

Planned for a future release to cover:

- Listener registration/unregistration
- Event dispatch and data correctness
- Multi-listener broadcasting

---

## 🚧 Roadmap

- [ ] ✅ Unit Tests – validate event flow and multi-listener registration  
- [ ] ✅ Async Event Support – allow event awaiters and `Task`-based dispatching  
- [ ] 🔄 Event Logging System – centralized event logger for debugging and history tracking  
- [ ] 🧠 Saga Pattern Support – orchestrate event-driven workflows using long-lived processes  
- [ ] 🔍 Editor Debug Tool – visualize live event dispatch and listener binding (EditorWindow)  
- [ ] 📦 DOTS-Compatible Version – leverage ECS-friendly architecture for performance  
- [ ] 🧪 Benchmark Tools – evaluate performance in large-scale systems  
- [ ] 🎨 Custom Inspector – interactive UI for testing and dispatching events manually  

---

## 📜 License

MIT License

---

## 👤 Author

Developed by [Little Kid]  
Feel free to contribute or submit issues on GitHub.