namespace PI.ConsoleEditor.MiniEngine.Screens;

public struct CoPixel
{
    private char _value;
    private ConsoleColor _coPixelColor;
    private ConsoleColor _bgColor;

    public char Value => _value;
    public ConsoleColor CoPixelColor => _coPixelColor;
    public ConsoleColor BgColor => _bgColor;

    public CoPixel(char value, ConsoleColor coPixelColor, ConsoleColor bgColor)
    {
        _value = value;
        _coPixelColor = coPixelColor;
        _bgColor = bgColor;
    }
}
