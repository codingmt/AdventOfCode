namespace AOC._2022;

internal class Day01 : DayBase
{
    public override void Part1()
    {
        var lines = GetInputLines();
        int total = 0, max = 0;
        for(var i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
            {
                if (total > max) max = total;
                total = 0;
            }
            else
                total += int.Parse(lines[i]);
        }

        if (total > max) max = total;

        Answer(max);
    }

    public override void Part2()
    {
        var lines = GetInputLines();
        var totals = new List<int>();
        var total = 0;
        for(var i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
            {
                totals.Add(total);
                total = 0;
            }
            else
                total += int.Parse(lines[i]);
        }

        totals.Add(total);

        Answer(totals.OrderByDescending(x => x).Take(3).Sum());
    }
}
