using PI.ConsoleEditor.MiniEngine;

ScreenManager screenManager = new ScreenManager(50, 50);
screenManager.Run();

EventQueue eventQueue = new EventQueue();
InputHandler inputHandler = new InputHandler(eventQueue);
inputHandler.Run();

CustomEventHandler eventHandler = new CustomEventHandler(eventQueue, screenManager);
eventHandler.Run();

while (!GlobalState.IsApplicationCloseRequested)
{
    Logger.Log("Main loop");
    Thread.Sleep(500);
}

Terminate();

void Terminate()
{

    Logger.Log("Terminate");
    Console.CursorVisible = true;
    Console.Clear();
}