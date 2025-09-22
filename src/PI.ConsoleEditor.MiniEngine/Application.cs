using PI.ConsoleEditor.MiniEngine.Events;
using PI.ConsoleEditor.MiniEngine.Loggers;
using PI.ConsoleEditor.MiniEngine.Screens;

namespace PI.ConsoleEditor.MiniEngine;

public class Application
{
    public Application()
    {
    }

    public async Task Run()
    {
        await ApplicationLifecycle.Instance.WaitForClose();
    }
}
