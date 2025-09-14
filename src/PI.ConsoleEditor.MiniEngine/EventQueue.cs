using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PI.ConsoleEditor.MiniEngine;

//ToDo: Make 2 interfaces: one expose writing, another reading
public class EventQueue
{
    //ToDo: singleton

    private ConcurrentQueue<CustomEvent> _events;

    public EventQueue()
    {
        _events = new ConcurrentQueue<CustomEvent>();
    }

    public void Enqueue(CustomEvent newEvent)
    {
        Logger.Log("Enqueue");
        _events.Enqueue(newEvent);
    }

    public CustomEvent Dequeue()
    {
        if (_events.TryDequeue(out CustomEvent result))
        {
            Logger.Log("Dequeue");
            return result;
        }

        return CustomEvent.NoneEvent;
    }
}
