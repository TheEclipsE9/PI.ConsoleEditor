namespace PI.ConsoleEditor.MiniEngine.Screens;

public class Screen
{
    private int _rows;
    public int Rows => _rows;
    private int _columns;
    public int Columns => _columns;


    private CoPixel[,] _backBuffer;
    private CoPixel[,] _frontBuffer;

    private bool _needToUpdateScreen;
    private readonly AutoResetEvent _updateSignal;

    public Screen(int rows, int columns, bool debugMode = false)
    {
        if (debugMode)
        {
            rows++;
            columns++;
        }

        _rows = rows;
        _columns = columns;

        _backBuffer = new CoPixel[rows, columns];
        _frontBuffer = new CoPixel[rows, columns];

        _needToUpdateScreen = true;
        _updateSignal = new AutoResetEvent(false);

        Initialize(debugMode);
    }

    private void Initialize(bool debugMode)
    {
        Console.Clear();
        Console.CursorVisible = true;
        Console.SetWindowSize(Columns, Rows);

        if (debugMode)
        {
            for (int column = 1; column < Columns; column++)
            {
                var fgColor = ConsoleColor.White;
                if (column % 10 == 0) fgColor = ConsoleColor.Red;

                _backBuffer[0, column] = new CoPixel((char)('0' + column % 10), fgColor, ConsoleColor.Black);
            }

            for (int row = 1; row < Rows; row++)
            {
                var fgColor = ConsoleColor.White;
                if (row % 10 == 0) fgColor = ConsoleColor.Red;

                _backBuffer[row, 0] = new CoPixel((char)('0' + row % 10), fgColor, ConsoleColor.Black);
            }
        }

        UpdateScreen();
    }

    public void UpdateScreen()
    {
        if (_needToUpdateScreen == false)
        {
            return;
        }

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                var backCoPixel = _backBuffer[i, j];
                var frontCoPixel = _frontBuffer[i, j];

                //boxing — implement IEquatable<T> to avoid
                if (!frontCoPixel.Equals(backCoPixel))
                {
                    _frontBuffer[i, j] = backCoPixel;
                    UpdateCoPixelOnScreen(i, j, backCoPixel);
                }
            }
        }

        _needToUpdateScreen = false;
    }

    private void UpdateCoPixelOnScreen(int row, int column, CoPixel coPixel)
    {
        var curForegroundColor = Console.ForegroundColor;
        var curBackgroundColor = Console.BackgroundColor;

        Console.ForegroundColor = coPixel.CoPixelColor;
        Console.BackgroundColor = coPixel.BgColor;
        Console.SetCursorPosition(column, row);
        Console.Write(coPixel.Value);

        Console.ForegroundColor = curForegroundColor;
        Console.BackgroundColor = curBackgroundColor;
    }

    public void DrawCoPixel(int row, int column, CoPixel coPixel)
    {
        if (row < 0 || row >= _rows) throw new ArgumentException("Out of height");
        if (column < 0 || column >= _columns) throw new ArgumentException("Out of width");

        _backBuffer[row, column] = coPixel;
        _needToUpdateScreen = true;
        _updateSignal.Set();
    }

    public void DrawCoPixelsHorizontaly(int row, int startColumn, IEnumerable<CoPixel> coPixels)
    {
        if (row < 0 || row >= _rows) throw new ArgumentException("Out of height");

        int column = startColumn;
        bool needToUpdateScreen = false;
        foreach (var coPixel in coPixels)
        {
            if (0 <= column && column < _columns)
            {
                _backBuffer[row, column] = coPixel;
                needToUpdateScreen = true;
            }
            column++;
        }

        _needToUpdateScreen = needToUpdateScreen;
        _updateSignal.Set();
    }
}
