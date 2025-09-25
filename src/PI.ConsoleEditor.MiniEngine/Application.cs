using PI.ConsoleEditor.MiniEngine.Events;
using PI.ConsoleEditor.MiniEngine.Screens;

namespace PI.ConsoleEditor.MiniEngine;

public class Application
{
    public Application()
    {
    }

    public async Task Run()
    {
        var screen = new Screen(10, 50, true);
        var globalEventQueue = new EventQueue();

        var inputHandler = new InputHandler(globalEventQueue);
        inputHandler.Run();

        var eventQueueHandler = new EventQueueHandler(globalEventQueue);
        eventQueueHandler.Run();

        await ApplicationLifecycle.Instance.WaitForClose();
    }
}
