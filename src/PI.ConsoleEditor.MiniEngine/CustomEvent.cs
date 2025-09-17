using System;

namespace PI.ConsoleEditor.MiniEngine;

public class CustomEvent
{
    private readonly CustomEventType _eventType;
    private readonly ICustomEventContext? _eventContext;

    public CustomEventType EventType => _eventType;
    public ICustomEventContext? EventContext => _eventContext;

    private static CustomEvent _noneEvent = new CustomEvent(CustomEventType.None);
    public static CustomEvent NoneEvent => _noneEvent;

    public CustomEvent(CustomEventType eventType, ICustomEventContext? eventContext = null)
    {
        _eventType = eventType;
        _eventContext = eventContext;
    }
}

public enum CustomEventType
{
    None,
    Log,
    ApplicationClose,
    Draw,
    Clear,
}