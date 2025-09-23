using PI.ConsoleEditor.MiniEngine.Screens;

namespace PI.ConsoleEditor.MiniEngine.Events;

public class EventQueueHandler
{
    private readonly EventQueue _eventQueue;

    public EventQueueHandler(EventQueue eventQueue)
    {
        _eventQueue = eventQueue;
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
                        return;
                }
            }
        });
    }
}
