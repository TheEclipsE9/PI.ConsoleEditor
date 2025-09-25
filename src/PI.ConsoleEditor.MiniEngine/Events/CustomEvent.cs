using System;

namespace PI.ConsoleEditor.MiniEngine.Events;

public class CustomEvent
{
    private readonly CustomEventType _eventType;
    private readonly ICustomEventContext? _eventContext;

    public CustomEventType EventType => _eventType;
    public ICustomEventContext? EventContext => _eventContext;

    private static readonly CustomEvent _noneEvent = new CustomEvent(CustomEventType.None);
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
    ApplicationClose,
    FillIn
}