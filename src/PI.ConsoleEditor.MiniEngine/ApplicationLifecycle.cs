using System;

namespace PI.ConsoleEditor.MiniEngine;

public class ApplicationLifecycle
{
    private static readonly Lazy<ApplicationLifecycle> _instance = new Lazy<ApplicationLifecycle>(() => new ApplicationLifecycle());
    public static ApplicationLifecycle Instance = _instance.Value;

    private ApplicationLifecycle() { }

    private readonly TaskCompletionSource<bool> _closeCompletionSource = new TaskCompletionSource<bool>();

    public async Task<bool> WaitForApplicationClose() => await _closeCompletionSource.Task.WaitAsync(CancellationToken.None);

    private volatile bool _isApplicationCloseRequested = false;
    public bool IsApplicationCloseRequested => _isApplicationCloseRequested;

    public void Close()
    {
        _isApplicationCloseRequested = true;
        _closeCompletionSource.TrySetResult(true);
    }
}
