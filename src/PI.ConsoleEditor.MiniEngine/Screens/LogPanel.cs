namespace PI.ConsoleEditor.MiniEngine.Screens;

public class LogPanel
{
    private int _originRow;
    private int _originColumn;
    private int _width;
    private int _height;

    private readonly Screen _screen;

    private Queue<string> _logMessages;

    public LogPanel(int originRow, int originColumn, int width, int height, Screen screen)
    {
        _originRow = originRow;
        _originColumn = originColumn;
        _width = width;
        _height = height;
        _screen = screen;
        _logMessages = new Queue<string>();
    }

    public void Log(string message)
    {
        _logMessages.Enqueue(message);

        if (_logMessages.Count >= _height)
        {
            _logMessages.Dequeue();
            int i = 0;
            foreach (var logMessage in _logMessages)
            {
                if (!(i < _logMessages.Count - 1)) break;

                var coPixelsForOldMessage = GetCoPixelsFromString(logMessage);
                _screen.DrawCoPixelsHorizontaly(_originRow + i, _originColumn, coPixelsForOldMessage);

                i++;
            }
            return;
        }
        var row = _logMessages.Count == 0 ? _originRow : _originRow + _logMessages.Count;

        var coPixelsForNewMessage = GetCoPixelsFromString(message);
        _screen.DrawCoPixelsHorizontaly(row, _originColumn, coPixelsForNewMessage);
    }

    private IEnumerable<CoPixel> GetCoPixelsFromString(string message)
        => message.Select(x => new CoPixel(x, ConsoleColor.Black, ConsoleColor.White));
}