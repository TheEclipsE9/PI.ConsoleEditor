using PI.ConsoleEditor.MiniEngine.Screens;

namespace PI.ConsoleEditor.MiniEngine.Events;

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
        //Task.Run returns a task that isn’t awaited.
        //Could store it in a field and await it during shutdown for graceful cleanup.
        Task.Run(() =>
        {
            while (!ApplicationLifecycle.Instance.IsApplicationCloseRequested)
            {
                var customEvent = _eventQueue.DequeueOrWait();

                switch (customEvent.EventType)
                {
                    case CustomEventType.Clear:
                        HandleClear();
                        break;
                    case CustomEventType.Draw:
                        HandleDraw();
                        break;
                    case CustomEventType.None:
                        break;
                    case CustomEventType.ApplicationClose:
                        ApplicationLifecycle.Instance.Close();
                        return;
                }
            }
        });
    }

    private void HandleDraw()
    {
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
        for (int i = 0; i < _screenManager.Rows; i++)
        {
            for (int j = 0; j < _screenManager.Columns - 25; j++)
            {
                _screenManager.DrawLowerHalfBlock(i, j, coPixelColor: ConsoleColor.Blue, bgColor: ConsoleColor.Green);
            }
        }
    }
}
