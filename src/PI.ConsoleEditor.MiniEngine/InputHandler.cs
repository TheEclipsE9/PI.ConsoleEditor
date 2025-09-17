using System;
using System.Diagnostics;

namespace PI.ConsoleEditor.MiniEngine;

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
        Task.Run(() =>
        {
            while (!ApplicationLifecycle.Instance.IsApplicationCloseRequested)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Q:
                        _logger.Log("ConsoleKey.Q pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(EventType.ApplicationClose));
                        return;
                    case ConsoleKey.D:
                        _logger.Log("ConsoleKey.D pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(EventType.Draw));
                        break;
                    case ConsoleKey.C:
                        _logger.Log("ConsoleKey.C pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(EventType.Clear));
                        break;
                    case ConsoleKey.L:
                        _logger.Log("ConsoleKey.C pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(EventType.Log, new LogEventContext("ConsoleKey.L pressed")));
                        break;
                }
            }
        });
    }
}
