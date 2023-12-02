﻿namespace AOC._2022;

internal class Day02 : DayBase
{
    public override void Part1()
    {
        var lines = GetInputLines();
        var scores = new Dictionary<string, int>
        {
            { "A X", 4 },
            { "A Y", 8 },
            { "A Z", 3 },
            { "B X", 1 },
            { "B Y", 5 },
            { "B Z", 9 },
            { "C X", 7 },
            { "C Y", 2 },
            { "C Z", 6 }
        };

        Answer(lines.Sum(x => scores[x]));
    }

    public override void Part2()
    {
        var lines = GetInputLines();
        var scores = new Dictionary<string, int>
        {
            { "A X", 3 },
            { "A Y", 4 },
            { "A Z", 8 },
            { "B X", 1 },
            { "B Y", 5 },
            { "B Z", 9 },
            { "C X", 2 },
            { "C Y", 6 },
            { "C Z", 7 }
        };

        Answer(lines.Sum(x => scores[x]));
    }
}
