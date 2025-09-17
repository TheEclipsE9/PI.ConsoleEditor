using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PI.ConsoleEditor.MiniEngine;

//ToDo: Make 2 interfaces: one expose writing, another reading
public class EventQueue
{
    //ToDo: singleton
    //ToDo: use Channel<T> for async
    //Note: try EventWaitHandle to prevent busy waiting, when need communication between threads

    private BlockingCollection<CustomEvent> _events;

    public EventQueue()
    {
        _events = new BlockingCollection<CustomEvent>();
    }

    public void EnqueueOrWait(CustomEvent newEvent)
    {
        _events.Add(newEvent);
    }

    public CustomEvent DequeueOrWait()
    {
        return _events.Take();
    }
}
