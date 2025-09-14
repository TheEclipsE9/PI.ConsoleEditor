using System;

namespace PI.ConsoleEditor.MiniEngine;

//ToDo: rename since event is a keyword
public class CustomEvent
{
    private readonly EventType _eventType;

    public EventType EventType => _eventType;

    private static CustomEvent _noneEvent = new CustomEvent(EventType.None);
    public static CustomEvent NoneEvent => _noneEvent;

    public CustomEvent(EventType eventType)
    {
        _eventType = eventType;
    }
}

public enum EventType
{
    None,
    ApplicationClose,
    Draw,
    Clear,
}