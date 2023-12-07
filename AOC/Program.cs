using AOC._2023;
using System.Diagnostics;

namespace AOC;

internal class Program
{
    [STAThread]
    private static void Main()
    {
        var day = new Day07();

        var stopwatch = new Stopwatch();
        
        stopwatch.Start();
        day.Part1();
        stopwatch.Stop();
        Console.WriteLine($"Part 1 answered in {stopwatch.Elapsed.TotalMinutes:0}:{stopwatch.Elapsed:ss\\.fff}.");
        Console.WriteLine();

        stopwatch.Restart();
        day.Part2();
        stopwatch.Stop();
        Console.WriteLine($"Part 2 answered in {stopwatch.Elapsed.TotalMinutes:0}:{stopwatch.Elapsed:ss\\.fff}.");
        Console.WriteLine();
    }
}