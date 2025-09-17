using System;
using System.Diagnostics;

namespace PI.ConsoleEditor.MiniEngine;

//ToDo: naming
public class CustomEventHandler
{
    private readonly EventQueue _eventQueue;
    private readonly ScreenManager _screenManager;
    private readonly ILogger _logger;
    private readonly LogPanel _logPanel;

    public CustomEventHandler(EventQueue eventQueue, ScreenManager screenManager, ILogger logger, LogPanel logPanel)
    {
        _eventQueue = eventQueue;
        _screenManager = screenManager;
        _logger = logger;
        _logPanel = logPanel;
    }

    public void Run()
    {
        Task.Run(() =>
        {
            while (!ApplicationLifecycle.Instance.IsApplicationCloseRequested)
            {
                var customEvent = _eventQueue.DequeueOrWait();

                switch (customEvent.EventType)
                {
                    case EventType.Clear:
                        HandleClear();
                        break;
                    case EventType.Draw:
                        HandleDraw();
                        break;
                    case EventType.None:
                        break;
                    case EventType.Log:
                        HandleLog(customEvent);
                        break;
                    case EventType.ApplicationClose:
                        ApplicationLifecycle.Instance.Close();
                        return;
                }
            }

            //_logger.Log("End CustomEventHandler");
        });
    }

    private void HandleDraw()
    {
        //_logger.Log("Handle draw");
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
        //_logger.Log("Handle clear");
        for (int i = 0; i < _screenManager.Rows; i++)
        {
            for (int j = 0; j < _screenManager.Columns - 25; j++)
            {
                _screenManager.DrawLowerHalfBlock(i, j, coPixelColor: ConsoleColor.Blue, bgColor: ConsoleColor.Green);
            }
        }
    }

    private void HandleLog(CustomEvent customEvent)
    {
        var context = customEvent.EventContext as LogEventContext;

        if (context is null) return;

        _logPanel.Log(context.Message);
    }
}
