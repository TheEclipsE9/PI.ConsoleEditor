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
            while (!GlobalState.IsApplicationCloseRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key; // true = don’t echo
                    switch (key)
                    {
                        case ConsoleKey.E:
                            Logger.Log("ConsoleKey.E pressed");
                            _eventQueue.Enqueue(new CustomEvent(EventType.ApplicationClose));
                            return;
                        case ConsoleKey.D:
                            Logger.Log("ConsoleKey.D pressed");
                            _eventQueue.Enqueue(new CustomEvent(EventType.Draw));
                            break;
                        case ConsoleKey.C:
                            Logger.Log("ConsoleKey.C pressed");
                            _eventQueue.Enqueue(new CustomEvent(EventType.Clear));
                            break;
                    }
                }
            }
        });
    }
}
