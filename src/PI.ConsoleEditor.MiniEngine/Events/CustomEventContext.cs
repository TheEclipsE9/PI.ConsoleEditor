using System;

namespace PI.ConsoleEditor.MiniEngine.Events;

public interface ICustomEventContext { }
public class LogEventContext : ICustomEventContext
{
    private string _message;
    public string Message => _message;

    public LogEventContext(string message)
    {
        _message = message;
    }
}
