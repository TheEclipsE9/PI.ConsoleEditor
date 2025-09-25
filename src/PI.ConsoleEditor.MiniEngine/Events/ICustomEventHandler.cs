using System;

namespace PI.ConsoleEditor.MiniEngine.Events;

public interface ICustomEventHandler
{
    void Handle(CustomEvent customEvent);
}
