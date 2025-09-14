using System;
using System.Diagnostics;

namespace PI.ConsoleEditor.MiniEngine;

public class InputHandler
{
    private readonly EventQueue _eventQueue;

    public InputHandler(EventQueue eventQueue)
    {
        _eventQueue = eventQueue;
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
                        Logger.Log("ConsoleKey.Q pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(EventType.ApplicationClose));
                        return;
                    case ConsoleKey.D:
                        Logger.Log("ConsoleKey.D pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(EventType.Draw));
                        break;
                    case ConsoleKey.C:
                        Logger.Log("ConsoleKey.C pressed");
                        _eventQueue.EnqueueOrWait(new CustomEvent(EventType.Clear));
                        break;
                }
            }
        });
    }
}
