const int WindowWidth = 50;
const int WindowHight = 50;

Initialize();
Thread.Sleep(10_000);

for (int i = 0; i < 50; i++)
{
    for (int j = 0; j < 50; j++)
    {
        SpecialMethods.DrawPixel(i, j, ConsoleColor.Blue);
    }
}

while (true)
{
    if (Console.ReadKey().Key == ConsoleKey.E)
    {
        break;
    }
    Thread.Sleep(1_000);
}

Terminate();

void Initialize()
{
    Console.Clear();
    Console.CursorVisible = false;
    Console.SetWindowSize(WindowWidth, WindowHight);
}

void Terminate()
{
    Console.Clear();
}

internal static class SpecialSymbols
{
    public static char SolidPixel = '\u2588';
}

internal static class SpecialMethods
{
    public static void DrawPixel(int left, int top, ConsoleColor color)
    {
        if (left < 0 || left > 50) throw new ArgumentException("Out of width");
        if (top < 0 || top > 50) throw new ArgumentException("Out of hight");

        var curForegroundColor = Console.ForegroundColor;
        var curCursorPosition = Console.GetCursorPosition();

        Console.ForegroundColor = color;
        Console.SetCursorPosition(left, top);
        Console.Write(SpecialSymbols.SolidPixel);

        Console.ForegroundColor = curForegroundColor;
        Console.SetCursorPosition(curCursorPosition.Left, curCursorPosition.Top);
    }
}