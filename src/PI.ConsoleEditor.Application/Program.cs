using System.Diagnostics.CodeAnalysis;

Initialize();
Thread.Sleep(1_000);

for (int i = 0; i < Screen.WindowWidth; i++)
{
    for (int j = 0; j < Screen.WindowHeight; j++)
    {
        Screen.DrawLowerHalfBlock(i, j, coPixelColor: ConsoleColor.Blue, bgColor: ConsoleColor.Green);
    }
}

int temp = 0;
int row1 = 25;
int row2 = 10;
while (true)
{
    Screen.UpdateScreen();
    if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.E)
    {
        break;
    }

    temp = temp + 1;
    if (temp == 10)
    {
        temp = 0;
    }

    if (temp % 2 == 0)
    {
        for (int c = 10; c < 20; c++)
        {
            Screen.DrawBlock(c, row1, ConsoleColor.Black, ConsoleColor.Black);
            Screen.DrawBlock(c, row2, ConsoleColor.Black, ConsoleColor.Black);
        }
    }
    else
    {
        for (int c = 10; c < 20; c++)
        {
            Screen.DrawLowerHalfBlock(c, row1, coPixelColor: ConsoleColor.Blue, bgColor: ConsoleColor.Green);
            Screen.DrawLowerHalfBlock(c, row2, coPixelColor: ConsoleColor.Blue, bgColor: ConsoleColor.Green);
        }
    }

    Thread.Sleep(500);
}

Terminate();

void Initialize()
{
    Console.Clear();
    Console.CursorVisible = false;
    Console.SetWindowSize(Screen.WindowWidth, Screen.WindowHeight);
}

void Terminate()
{
    Console.Clear();
}

public struct CoPixel
{
    private char _value;
    private ConsoleColor _coPixelColor;
    private ConsoleColor _bgColor;

    public char Value => _value;
    public ConsoleColor CoPixelColor => _coPixelColor;
    public ConsoleColor BgColor => _bgColor;

    public CoPixel(char value, ConsoleColor coPixelColor, ConsoleColor bgColor)
    {
        _value = value;
        _coPixelColor = coPixelColor;
        _bgColor = bgColor;
    }
}

internal static class Screen
{
    public const int WindowWidth = 50;
    public const int WindowHeight = 50;
    private readonly static CoPixel[,] _backBuffer = new CoPixel[WindowWidth, WindowHeight];
    private readonly static CoPixel[,] _frontBuffer = new CoPixel[WindowWidth, WindowHeight];

    //have an event/trigger like wait, when smne call DrawPixel , updte screen is triggered, otherwise no need for constant screen redrawing
    private static bool _needToUpdateScreen = true;

    public static void UpdateScreen()
    {
        if (_needToUpdateScreen == false)
        {
            return;
        }

        for (int i = 0; i < Screen.WindowWidth; i++)
        {
            for (int j = 0; j < Screen.WindowHeight; j++)
            {
                var backBufferValue = _backBuffer[i, j];
                var frontBufferValue = _frontBuffer[i, j];

                //boxing — implement IEquatable<T> to avoid
                if (!frontBufferValue.Equals(backBufferValue))
                {
                    _frontBuffer[i, j] = backBufferValue;
                    UpdateCoPixelOnScreen(i, j, backBufferValue);
                }
            }
        }

        _needToUpdateScreen = false;
    }

    public static void DrawBlock(int left, int top, ConsoleColor coPixelColor, ConsoleColor bgColor)
    {
        if (left < 0 || left >= Screen.WindowWidth) throw new ArgumentException("Out of width");
        if (top < 0 || top >= Screen.WindowHeight) throw new ArgumentException("Out of height");

        var newCoPixel = new CoPixel(SpecialSymbols.SolidBlcok, coPixelColor: coPixelColor, bgColor: bgColor);

        DrawCoPixel(left, top, newCoPixel);
    }

    public static void DrawLowerHalfBlock(int left, int top, ConsoleColor coPixelColor, ConsoleColor bgColor)
    {
        if (left < 0 || left >= Screen.WindowWidth) throw new ArgumentException("Out of width");
        if (top < 0 || top >= Screen.WindowHeight) throw new ArgumentException("Out of height");

        var newCoPixel = new CoPixel(SpecialSymbols.SolidLowerHalfBlcok, coPixelColor: coPixelColor, bgColor: bgColor);

        DrawCoPixel(left, top, newCoPixel);
    }

    private static void DrawCoPixel(int left, int top, CoPixel coPixel)
    {
        _backBuffer[left, top] = coPixel;
        _needToUpdateScreen = true;
    }

    private static void UpdateCoPixelOnScreen(int left, int top, CoPixel coPixel)
    {
        var curForegroundColor = Console.ForegroundColor;
        var curBackgroundColor = Console.BackgroundColor;
        var curCursorPosition = Console.GetCursorPosition();

        Console.ForegroundColor = coPixel.CoPixelColor;
        Console.BackgroundColor = coPixel.BgColor;
        Console.SetCursorPosition(left, top);
        Console.Write(coPixel.Value);

        Console.ForegroundColor = curForegroundColor;
        Console.BackgroundColor = curBackgroundColor;
        Console.SetCursorPosition(curCursorPosition.Left, curCursorPosition.Top);
    }
}

internal static class SpecialSymbols
{
    public static char SolidBlcok = '\u2588';
    public static char SolidLowerHalfBlcok = '\u2584';
    public static char SolidUpperHalfBlcok = '\u2580';
}