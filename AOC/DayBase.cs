namespace AOC;

internal abstract class DayBase
{
    public abstract void Part1();

    public abstract void Part2();

    protected TextReader GetInputReader(string filename = null) =>
        File.OpenText(@$"{GetType().Namespace.Right(4)}\input\{GetType().Name}{filename}.txt");

    protected string GetInput(string filename = null) => 
        File.ReadAllText(@$"{GetType().Namespace.Right(4)}\input\{GetType().Name}{filename}.txt");

    protected string[] GetInputLines(string filename = null) => 
        GetInput(filename).Split("\r\n");

    protected static void Answer(object answer)
    {
        Clipboard.SetText(answer.ToString());
        Console.WriteLine("Answer sent to clipboard: " + answer);
    }
}

