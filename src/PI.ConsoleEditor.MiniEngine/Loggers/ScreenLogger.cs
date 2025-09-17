using PI.ConsoleEditor.MiniEngine.Events;

namespace PI.ConsoleEditor.MiniEngine.Loggers;

public class ScreenLogger : ILogger
{
    private readonly EventQueue _eventQueue;

    public ScreenLogger(EventQueue eventQueue)
    {
        _eventQueue = eventQueue;
    }

    public void Log(string message)
    {
        _eventQueue.EnqueueOrWait(new CustomEvent(CustomEventType.Log, new LogEventContext(message)));
    }
}
