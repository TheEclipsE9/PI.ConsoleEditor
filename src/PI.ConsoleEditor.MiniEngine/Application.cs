using System;
using System.Threading.Tasks;

namespace PI.ConsoleEditor.MiniEngine;

public class Application
{
    private readonly EventQueue _eventQueue;
    private readonly ILogger _logger;

    private int w;
    private int h;


    public Application()
    {
        _eventQueue = new EventQueue();
        _logger = new ScreenLogger(_eventQueue);

        w = Console.WindowWidth;
        h = Console.WindowHeight;
    }

    public async Task Run()
    {
        //_logger.Log("Run");

        OnStart();

        await ApplicationLifecycle.Instance.WaitForApplicationClose();

        OnClose();
    }

    private void OnStart()
    {
        //_logger.Log("OnStart");
        ScreenManager screenManager = new ScreenManager(50, 75, _logger);
        screenManager.Run();

        InputHandler inputHandler = new InputHandler(_eventQueue, _logger);
        inputHandler.Run();

        var logPanel = new LogPanel(0, 50, 25, 50, screenManager.Screen);
        CustomEventHandler eventHandler = new CustomEventHandler(_eventQueue, screenManager, _logger, logPanel);
        eventHandler.Run();
    }

    private void OnClose()
    {
        //_logger.Log("OnClose");
        Console.CursorVisible = true;
        Console.WindowWidth = w;
        Console.WindowHeight = h;
        Console.Clear();
    }
}
