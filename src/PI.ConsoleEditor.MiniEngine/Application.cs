using System;
using System.Threading.Tasks;

namespace PI.ConsoleEditor.MiniEngine;

//ToDo: if will have interface for screen handlers etc, can make a builder, so can pass diffimpl of scree/handlers then build app, and run
public class Application
{
    public async Task Run()
    {
        Logger.Log("Run");

        OnStart();

        await ApplicationLifecycle.Instance.WaitForApplicationClose();

        OnClose();
    }

    private void OnStart()
    {
        Logger.Log("OnStart");
        ScreenManager screenManager = new ScreenManager(50, 50);
        screenManager.Run();

        EventQueue eventQueue = new EventQueue();
        InputHandler inputHandler = new InputHandler(eventQueue);
        inputHandler.Run();

        CustomEventHandler eventHandler = new CustomEventHandler(eventQueue, screenManager);
        eventHandler.Run();
    }

    private void OnClose()
    {
        Logger.Log("OnClose");
        Console.CursorVisible = true;
        Console.Clear();
    }
}
