using System;

namespace PI.ConsoleEditor.MiniEngine;

//ToDo: rename since event is a keyword
public class CustomEvent
{
    //pub sub pattern?
    //expose producesr have ref to event queue instance
    //consumers has to be added, when thread read event queue, it can map eventtype to consumer and call its handle method
    private readonly EventType _eventType;
    private readonly ICustomEventContext? _eventContext;

    public EventType EventType => _eventType;
    public ICustomEventContext? EventContext => _eventContext;

    private static CustomEvent _noneEvent = new CustomEvent(EventType.None);
    public static CustomEvent NoneEvent => _noneEvent;

    public CustomEvent(EventType eventType, ICustomEventContext? eventContext = null)
    {
        _eventType = eventType;
        _eventContext = eventContext;
    }
}

public enum EventType
{
    None,
    Log,
    ApplicationClose,
    Draw,
    Clear,
}