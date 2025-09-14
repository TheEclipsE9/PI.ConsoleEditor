using System;

namespace PI.ConsoleEditor.MiniEngine;

public static class GlobalState
{
    private static bool _isApplicationCloseRequested;
    public static bool IsApplicationCloseRequested => _isApplicationCloseRequested;

    static GlobalState()
    {
        _isApplicationCloseRequested = false;
    }

    public static void RequestApplicationClose()
    {
        _isApplicationCloseRequested = true;
    }
}
