using System;

namespace PI.ConsoleEditor.MiniEngine;

public static class Logger
{
    private static readonly string LogFolderPath = Directory.GetCurrentDirectory();
    private static readonly string LogFilePath = Path.Combine(LogFolderPath, "Logs.txt");

    private static object _lock = new object();

    public static void Log(string message)
    {
        //Note: consider spliting into layout and have log in right layout
        lock (_lock)
        {
            File.AppendAllLines(LogFilePath, new string[] { message });
        }
    }
}
