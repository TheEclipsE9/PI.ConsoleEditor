namespace PI.ConsoleEditor.MiniEngine;

public class Application
{
    private readonly EventQueue _eventQueue;
    private readonly ILogger _logger;

    public Application()
    {
        _eventQueue = new EventQueue();
        _logger = new ScreenLogger(_eventQueue);
    }

    public async Task Run()
    {
        OnStart();

        await ApplicationLifecycle.Instance.WaitForApplicationClose();

        OnClose();
    }

    private void OnStart()
    {
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
        Console.CursorVisible = true;
        Console.Clear();
    }
}
