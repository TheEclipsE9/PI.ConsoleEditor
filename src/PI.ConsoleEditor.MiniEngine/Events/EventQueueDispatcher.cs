using PI.ConsoleEditor.MiniEngine.Screens;

namespace PI.ConsoleEditor.MiniEngine.Events;

public class EventQueueDispatcher
{
    private readonly EventQueue _eventQueue;
    private Dictionary<CustomEventType, ICustomEventHandler> _eventHandlers;

    public EventQueueDispatcher(EventQueue eventQueue, IEnumerable<(CustomEventType, ICustomEventHandler)> eventHandlers)
    {
        _eventQueue = eventQueue;
        _eventHandlers = new Dictionary<CustomEventType, ICustomEventHandler>();
        foreach (var handler in eventHandlers)
        {
            _eventHandlers.Add(handler.Item1, handler.Item2);
        }
    }

    public void Run()
    {
        //Task.Run returns a task that isn’t awaited.
        //Could store it in a field and await it during shutdown for graceful cleanup.
        Task.Run(() =>
        {
            while (!ApplicationLifecycle.Instance.IsApplicationCloseRequested)
            {
                var customEvent = _eventQueue.DequeueOrWait();

                switch (customEvent.EventType)
                {
                    case CustomEventType.None:
                        break;
                    case CustomEventType.ApplicationClose:
                        ApplicationLifecycle.Instance.Close();
                        break;
                    case CustomEventType.FillIn:
                        if (_eventHandlers.TryGetValue(CustomEventType.FillIn, out ICustomEventHandler handler))
                        {
                            handler.Handle(customEvent);
                        }
                        break;
                }
            }
        });
    }
}
