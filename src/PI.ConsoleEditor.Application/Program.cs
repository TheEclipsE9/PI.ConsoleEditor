using System.Diagnostics.CodeAnalysis;
using PI.ConsoleEditor.MiniEngine;

ScreenManager screenManager = new ScreenManager(50, 50);
screenManager.Run();

for (int i = 0; i < screenManager.Rows; i++)
{
    for (int j = 0; j < screenManager.Columns; j++)
    {
        screenManager.DrawLowerHalfBlock(i, j, coPixelColor: ConsoleColor.Blue, bgColor: ConsoleColor.Green);
    }
}

int temp = 0;
int row1 = 25;
int row2 = 10;
while (true)
{
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
            screenManager.DrawBlock(c, row1, ConsoleColor.Black, ConsoleColor.Black);
            screenManager.DrawBlock(c, row2, ConsoleColor.Black, ConsoleColor.Black);
        }
    }
    else
    {
        for (int c = 10; c < 20; c++)
        {
            screenManager.DrawLowerHalfBlock(c, row1, coPixelColor: ConsoleColor.Blue, bgColor: ConsoleColor.Green);
            screenManager.DrawLowerHalfBlock(c, row2, coPixelColor: ConsoleColor.Blue, bgColor: ConsoleColor.Green);
        }
    }

    Thread.Sleep(500);
}

Terminate();

void Terminate()
{
    Console.Clear();
}