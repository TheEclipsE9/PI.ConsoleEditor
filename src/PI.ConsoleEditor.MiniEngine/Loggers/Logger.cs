namespace PI.ConsoleEditor.MiniEngine.Loggers;

public class LoggerWrap
{
    private readonly ILogger _logger;

    public LoggerWrap(ILogger logger)
    {
        _logger = logger;
    }

    public void Log(string message)
    {
        _logger.Log(message);
    }
}

public interface ILogger
{
    void Log(string message);
}