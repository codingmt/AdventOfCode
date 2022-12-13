namespace AOC;
internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var day = new Day12();
        day.Run1();
        day.Run2();
    }
}