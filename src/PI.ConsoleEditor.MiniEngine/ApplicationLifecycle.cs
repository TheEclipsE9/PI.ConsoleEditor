using System;

namespace PI.ConsoleEditor.MiniEngine;

public class ApplicationLifecycle
{
    private static readonly Lazy<ApplicationLifecycle> _instance = new Lazy<ApplicationLifecycle>(() => new ApplicationLifecycle());
    public static readonly ApplicationLifecycle Instance = _instance.Value;

    private ApplicationLifecycle()
    {
        _closeCompletionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    private readonly TaskCompletionSource<bool> _closeCompletionSource;

    public Task<bool> WaitForClose() => _closeCompletionSource.Task;

    private volatile bool _isApplicationCloseRequested = false;
    public bool IsApplicationCloseRequested => _isApplicationCloseRequested;

    public void Close()
    {
        _isApplicationCloseRequested = true;
        _closeCompletionSource.TrySetResult(true);
    }
}
