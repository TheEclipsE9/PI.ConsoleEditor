using System;
using PI.ConsoleEditor.MiniEngine.Screens;

namespace PI.ConsoleEditor.MiniEngine.Events;

public class FillInEventHandler : ICustomEventHandler
{
    private readonly Screen _screen;

    public FillInEventHandler(Screen screen)
    {
        _screen = screen;
    }

    public void Handle(CustomEvent customEvent)
    {
        var context = customEvent.EventContext as FillInEventContext;
        if (context is null) return;

        _screen.FillIn(context.BgColor);
    }
}
