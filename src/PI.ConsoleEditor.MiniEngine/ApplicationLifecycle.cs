using System;

namespace PI.ConsoleEditor.MiniEngine;

public class ApplicationLifecycle
{
    private static ApplicationLifecycle _instance;

    //private static readonly Lazy<ApplicationLifecycle> _instance = new Lazy<ApplicationLifecycle>(() => new ApplicationLifecycle());
    private static readonly Object _lock = new Object();
    public static ApplicationLifecycle Instance
    {
        get
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    if (_instance is null)
                    {
                        _instance = new ApplicationLifecycle();
                    }
                }
            }
            return _instance;
        }
    }

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
