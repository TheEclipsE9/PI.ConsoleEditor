using System;

namespace PI.ConsoleEditor.MiniEngine;

public class Logger
{
    private readonly ILogger _logger;

    public Logger(ILogger logger)
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