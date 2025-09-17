using PI.ConsoleEditor.MiniEngine;

Console.WriteLine("Press to start...");
Console.ReadKey();

var app = new Application();

//app.Layout
//      .AddPanel(origin: 0, width: 20, height: 30)
//      .AddPanel(origin: 20, width 30, height: 30)

//app.Input.
//      .AddHandler(keyPressed: Key.Tab, onKeyPressed: (context) =>
//      {
//          context.ActivePanel = context.NextPanel;
//          //change color
//          //rerender panel with mor wider borders
//      })

await app.Run();