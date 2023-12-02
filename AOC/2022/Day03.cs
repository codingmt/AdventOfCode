namespace AOC._2022;

internal class Day03 : DayBase
{
    public override void Part1()
    {
        var lines = GetInputLines();
        var total = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var common = findCommon(lines[i]);
            switch (common)
            {
                case >= 'a' and <= 'z':
                    total += 1 + common - 'a';
                    break;

                case >= 'A' and <= 'Z':
                    total += 27 + common - 'A';
                    break;

                default:
                    break;
            }
        }

        Answer(total);

        char findCommon(string line)
        {
            var halfway = line.Length / 2;
            for (int j = 0; j < halfway; j++)
            {
                for (int k = halfway; k < line.Length; k++)
                {
                    if (line[j] == line[k])
                        return line[j];
                }
            }

            return ' ';
        };
    }

    public override void Part2()
    {
        var lines = GetInputLines();
        var total = 0;
        for (int i = 0; i < lines.Length; i+=3)
        {
            var common = findCommon(lines[i], lines[i+1], lines[i+2]);
            switch (common)
            {
                case >= 'a' and <= 'z':
                    total += 1 + common - 'a';
                    break;

                case >= 'A' and <= 'Z':
                    total += 27 + common - 'A';
                    break;

                default:
                    break;
            }
        }

        Answer(total);

        char findCommon(string line1, string line2, string line3)
        {
            for (int j = 0; j < line1.Length; j++)
            {
                for (int k = 0; k < line2.Length; k++)
                {
                    for (int l = 0; l < line3.Length; l++)
                    {
                        if (line1[j] == line2[k] && line1[j] == line3[l])
                            return line1[j];
                    }
                }
            }

            return ' ';
        };
    }
}
