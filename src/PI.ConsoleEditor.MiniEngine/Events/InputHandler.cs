using PI.ConsoleEditor.MiniEngine.Loggers;

namespace PI.ConsoleEditor.MiniEngine.Events;

public class InputHandler
{
    private readonly EventQueue _eventQueue;
    private readonly ILogger _logger;

    public InputHandler(EventQueue eventQueue, ILogger logger)
    {
        _eventQueue = eventQueue;
        _logger = logger;
    }

    public void Run()
    {
        //Task.Run returns a task that isn’t awaited.
        //Could store it in a field and await it during shutdown for graceful cleanup.
        Task.Run(() =>
        {
            while (!ApplicationLifecycle.Instance.IsApplicationCloseRequested)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Q:
                        _logger.Log("ConsoleKey.Q pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(CustomEventType.ApplicationClose));
                        return;
                    case ConsoleKey.D:
                        _logger.Log("ConsoleKey.D pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(CustomEventType.Draw));
                        break;
                    case ConsoleKey.C:
                        _logger.Log("ConsoleKey.C pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(CustomEventType.Clear));
                        break;
                    case ConsoleKey.L:
                        _logger.Log("ConsoleKey.L pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(CustomEventType.Log, new LogEventContext("ConsoleKey.L pressed")));
                        break;
                }
            }
        });
    }
}
