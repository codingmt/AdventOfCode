namespace AOC._2022;

internal class Day05 : DayBase
{
    public override void Part1()
    {
        var stacks = GetStacks();
        var lines = GetInputLines();
        for (int i = 0; i < lines.Length; i++)
        {
            var (count, from, to) = Parse(lines[i]);
            for (int j = 0; j < count; j++)
            {
                var cnt = stacks[from - 1].Count;
                var item = stacks[from - 1][cnt - 1];
                stacks[from - 1].RemoveAt(cnt - 1);
                stacks[to - 1].Add(item);
            }
        }

        Answer(stacks.Aggregate("", (r, s) => r + s.Last()));
    }

    public override void Part2()
    {
        var stacks = GetStacks();
        var lines = GetInputLines();
        for (int i = 0; i < lines.Length; i++)
        {
            var (count, from, to) = Parse(lines[i]);
            var cnt = stacks[from - 1].Count;
            var items = stacks[from - 1].GetRange(cnt-count, count);
            stacks[from - 1].RemoveRange(cnt - count, count);
            stacks[to - 1].AddRange(items);
        }

        Answer(stacks.Aggregate("", (r, s) => r + s.Last()));
    }

    private List<char>[] GetStacks()
    {
        var lines = GetInputLines("stacks");
        var result = new List<List<char>>();
        for (int i = 1; i < lines.Last().Length; i += 4)
            result.Add(new List<char>());

        for (int i = lines.Length - 1; i >= 0; i--)
            for (int j = 0; j < lines[i].Length; j += 4)
            {
                var c = lines[i][j + 1];
                if (c != ' ')
                    result[j / 4].Add(lines[i][j + 1]); 
            }

        return result.ToArray();
    }

    private (int count, int from, int to) Parse(string line)
    {
        var parts = line.Split(' ');
        return (int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]));
    }
}
