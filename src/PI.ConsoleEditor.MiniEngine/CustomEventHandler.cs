using System;
using System.Diagnostics;

namespace PI.ConsoleEditor.MiniEngine;

//ToDo: naming
public class CustomEventHandler
{
    private readonly EventQueue _eventQueue;
    private readonly ScreenManager _screenManager;

    public CustomEventHandler(EventQueue eventQueue, ScreenManager screenManager)
    {
        _eventQueue = eventQueue;
        _screenManager = screenManager;
    }

    public void Run()
    {
        Task.Run(() =>
        {
            bool isApplicationCloseRequested = false;
            while (!isApplicationCloseRequested)
            {
                var result = _eventQueue.Dequeue();

                switch (result.EventType)
                {
                    case EventType.Clear:
                        HandleClear();
                        break;
                    case EventType.Draw:
                        HandleDraw();
                        break;
                    case EventType.None:
                        break;
                    case EventType.ApplicationClose:
                        isApplicationCloseRequested = true;
                        GlobalState.RequestApplicationClose();
                        return;
                }
            }

            Logger.Log("End CustomEventHandler");
        });
    }

    private void HandleDraw()
    {
        Logger.Log("Handle draw");
        int row1 = 25;
        int row2 = 10;
        for (int c = 10; c < 20; c++)
        {
            _screenManager.DrawBlock(row1, c, ConsoleColor.Black, ConsoleColor.Black);
            _screenManager.DrawBlock(row2, c, ConsoleColor.Black, ConsoleColor.Black);
        }
    }

    private void HandleClear()
    {
        Logger.Log("Handle clear");
        for (int i = 0; i < _screenManager.Rows; i++)
        {
            for (int j = 0; j < _screenManager.Columns; j++)
            {
                _screenManager.DrawLowerHalfBlock(i, j, coPixelColor: ConsoleColor.Blue, bgColor: ConsoleColor.Green);
            }
        }
    }
}
