using System.Collections.Concurrent;

namespace PI.ConsoleEditor.MiniEngine;

public class EventQueue
{
    private BlockingCollection<CustomEvent> _events;

    public EventQueue()
    {
        _events = new BlockingCollection<CustomEvent>();
    }

    public void EnqueueOrWait(CustomEvent newEvent) => _events.Add(newEvent);

    public CustomEvent DequeueOrWait() => _events.Take();
}
