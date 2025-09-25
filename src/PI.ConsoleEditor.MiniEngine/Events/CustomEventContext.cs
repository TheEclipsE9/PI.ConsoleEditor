using System;

namespace PI.ConsoleEditor.MiniEngine.Events;

public interface ICustomEventContext { }

public class FillInEventContext : ICustomEventContext
{
    private readonly ConsoleColor _bgColor;
    public ConsoleColor BgColor => _bgColor;

    public FillInEventContext(ConsoleColor bgColor)
    {
        _bgColor = bgColor;
    }
}