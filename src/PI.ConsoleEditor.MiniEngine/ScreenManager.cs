using System;
using System.Diagnostics;

namespace PI.ConsoleEditor.MiniEngine;

public class ScreenManager
{
    private readonly Screen _screen;

    public (int Rows, int Columns) ScreenDimensions => (_screen.Rows, _screen.Columns);
    public int Rows => _screen.Rows;
    public int Columns => _screen.Columns;

    public ScreenManager(int rows, int columns)
    {
        _screen = new Screen(rows, columns);
    }

    public void Run()
    {
        Initialize();
        Task.Run(() =>
        {
            while (true)
            {
                //Note: Can event signal when need update
                _screen.UpdateScreen();
            }
        });
    }

    private void Initialize()
    {
        Console.Clear();
        Console.CursorVisible = true;
        Console.SetWindowSize(Columns, Rows);
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


public class Screen
{
    private int _rows;
    public int Rows => _rows;
    private int _columns;
    public int Columns => _columns;


    private CoPixel[,] _backBuffer;
    private CoPixel[,] _frontBuffer;

    private bool _needToUpdateScreen;

    public Screen(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;

        _backBuffer = new CoPixel[rows, columns];
        _frontBuffer = new CoPixel[rows, columns];

        _needToUpdateScreen = true;
    }

    public void UpdateScreen()
    {
        if (_needToUpdateScreen == false)
        {
            return;
        }

        Logger.Log("UpdateScreen");
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
        if (row < 0 || row >= _rows) throw new ArgumentException("Out of width");
        if (column < 0 || column >= _columns) throw new ArgumentException("Out of height");

        _backBuffer[row, column] = coPixel;
        _needToUpdateScreen = true;
    }
}