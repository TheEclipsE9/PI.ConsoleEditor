namespace PI.ConsoleEditor.MiniEngine.Loggers;

public class FileLogger : ILogger
{
    private readonly string LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs.txt");

    private object _lock = new object();

    public void Log(string message)
    {
        lock (_lock)
        {
            File.AppendAllLines(LogFilePath, new string[] { message });
        }
    }
}
