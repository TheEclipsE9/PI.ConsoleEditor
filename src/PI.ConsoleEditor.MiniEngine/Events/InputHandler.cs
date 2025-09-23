namespace PI.ConsoleEditor.MiniEngine.Events;

public class InputHandler
{
    private readonly EventQueue _eventQueue;

    public InputHandler(EventQueue eventQueue)
    {
        _eventQueue = eventQueue;
    }

    public void Run()
    {
        //Task.Run returns a task that isn’t awaited.
        //Could store it in a field and await it during shutdown for graceful cleanup.
        Task.Run(() =>
        {
            while (!ApplicationLifecycle.Instance.IsApplicationCloseRequested)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Q:
                        _eventQueue.EnqueueOrWait(new CustomEvent(CustomEventType.ApplicationClose));
                        return;
                    case ConsoleKey.C:
                        _eventQueue.EnqueueOrWait(new CustomEvent(CustomEventType.Clear));
                        break;
                }
            }
        });
    }
}
