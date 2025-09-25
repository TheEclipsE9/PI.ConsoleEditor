namespace PI.ConsoleEditor.MiniEngine.Screens;

public class Screen
{
    private readonly int _rowsOrigin;
    private int _rows;
    public int Rows => _rows;
    private readonly int _columnsOrigin;
    private int _columns;
    public int Columns => _columns;


    private CoPixel[,] _backBuffer;
    private CoPixel[,] _frontBuffer;

    public Screen(int rows, int columns, bool debugMode = false)
    {
        if (debugMode)
        {
            rows++;
            columns++;
            _rowsOrigin = 1;
            _columnsOrigin = 1;
        }

        _rows = rows;
        _columns = columns;

        _backBuffer = new CoPixel[rows, columns];
        _frontBuffer = new CoPixel[rows, columns];

        Initialize(debugMode);
    }

    private void Initialize(bool debugMode)
    {
        Console.Clear();
        Console.CursorVisible = true;
        Console.SetWindowSize(Columns, Rows);

        if (debugMode)
        {
            for (int row = _rowsOrigin; row < Rows; row++)
            {
                var fgColor = ConsoleColor.White;
                if (row % 10 == 0) fgColor = ConsoleColor.Red;

                _backBuffer[row, 0] = new CoPixel((char)('0' + row % 10), fgColor, ConsoleColor.Black);
            }

            for (int column = _columnsOrigin; column < Columns; column++)
            {
                var fgColor = ConsoleColor.White;
                if (column % 10 == 0) fgColor = ConsoleColor.Red;

                _backBuffer[0, column] = new CoPixel((char)('0' + column % 10), fgColor, ConsoleColor.Black);
            }
        }

        UpdateScreen();
    }

    public void UpdateScreen()
    {
        Console.CursorVisible = false;
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
        Console.CursorVisible = true;
    }

    public void FillIn(ConsoleColor bgColor)
    {
        for (int row = _rowsOrigin; row < Rows; row++)
        {
            for (int column = _columnsOrigin; column < Columns; column++)
            {
                var coPixel = new CoPixel(SpecialSymbols.SolidBlcok, bgColor, bgColor);
                _backBuffer[row, column] = coPixel;
            }
        }
        UpdateScreen();
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

        UpdateScreen();
    }

    public void DrawCoPixelsHorizontaly(int row, int startColumn, IEnumerable<CoPixel> coPixels)
    {
        if (row < 0 || row >= _rows) throw new ArgumentException("Out of height");

        int column = startColumn;
        foreach (var coPixel in coPixels)
        {
            if (0 <= column && column < _columns)
            {
                _backBuffer[row, column] = coPixel;
            }
            column++;
        }
        UpdateScreen();
    }
}
