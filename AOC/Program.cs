namespace AOC;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var day = new Day17();
        day.Part1();
        Console.WriteLine();
        day.Part2();
        Console.WriteLine();
    }
}