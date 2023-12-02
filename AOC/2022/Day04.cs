namespace AOC._2022;

internal class Day04 : DayBase
{
    public override void Part1()
    {
        var lines = GetInputLines();
        var count = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var (b1, e1, b2, e2) = Split(lines[i]);
            if ((b1 >= b2 && e1 <= e2) || (b2 >= b1 && e2 <= e1))
                count++;
        }

        Answer(count);
    }

    public override void Part2()
    {
        var lines = GetInputLines();
        var count = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var (b1, e1, b2, e2) = Split(lines[i]);
            if ((b1 >= b2 && b1 <= e2) || (b2 >= b1 && b2 <= e1))
                count++;
        }

        Answer(count);
    }

    private (int b1, int e1, int b2, int e2) Split(string line)
    {
        var parts = line.Split('-', ',');
        return (int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }
}
