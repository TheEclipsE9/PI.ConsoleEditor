using PI.ConsoleEditor.MiniEngine.Loggers;

namespace PI.ConsoleEditor.MiniEngine.Screens;

public class ScreenManager
{
    private readonly AutoResetEvent _updateSignal = new AutoResetEvent(false);
    private readonly Screen _screen;
    public Screen Screen => _screen;
    public (int Rows, int Columns) ScreenDimensions => (_screen.Rows, _screen.Columns);
    public int Rows => _screen.Rows;
    public int Columns => _screen.Columns;

    public ScreenManager(int rows, int columns, ILogger logger, bool debugMode)
    {
        _screen = new Screen(rows, columns, _updateSignal, logger, debugMode);
    }

    public void Run()
    {
        //Task.Run returns a task that isn’t awaited.
        //Could store it in a field and await it during shutdown for graceful cleanup.
        Task.Run(() =>
        {
            while (!ApplicationLifecycle.Instance.IsApplicationCloseRequested)
            {
                _updateSignal.WaitOne();
                _screen.UpdateScreen();
            }
        });
    }

    public void DrawBlock(int row, int column, ConsoleColor coPixelColor, ConsoleColor bgColor)
    {
        var newCoPixel = new CoPixel(SpecialSymbols.SolidBlcok, coPixelColor: coPixelColor, bgColor: bgColor);

        _screen.DrawCoPixel(row, column, newCoPixel);
    }

    public void DrawLowerHalfBlock(int row, int column, ConsoleColor coPixelColor, ConsoleColor bgColor)
    {
        var newCoPixel = new CoPixel(SpecialSymbols.SolidLowerHalfBlcok, coPixelColor: coPixelColor, bgColor: bgColor);

        _screen.DrawCoPixel(row, column, newCoPixel);
    }
}
