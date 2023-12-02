using AOC._2023;

namespace AOC;

internal class Program
{
    [STAThread]
    private static void Main()
    {
        var day = new Day02();
        day.Part1();
        Console.WriteLine();
        day.Part2();
        Console.WriteLine();
    }
}