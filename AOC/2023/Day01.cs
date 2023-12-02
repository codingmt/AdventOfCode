using System.Text.RegularExpressions;

namespace AOC._2023;

internal class Day01 : DayBase
{
    public override void Part1()
    {
        string[] inputLines = GetInputLines();
        var re = new Regex(@"\d");
        int sum = 0;
        foreach (string line in inputLines)
        {
            var matches = re.Matches(line);
            sum += int.Parse(matches.First().Value + matches.Last().Value);
        }
        Answer(sum);
    }

    public override void Part2()
    {
        var lookup = 
            new Dictionary<string, string>
            {
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" }
            };
        string parseMatch(string match) => 
            lookup.TryGetValue(match, out var digit) ? digit : match;

        string[] inputLines = GetInputLines();
        var re = new Regex(@"\d|one|two|three|four|five|six|seven|eight|nine");
        int sum = 0;
        foreach (string line in inputLines)
        {
            var match = re.Match(line);
            var part = int.Parse(
                parseMatch(match.Value) + 
                parseMatch(LastMatchOf(line, re).Value));
            sum += part;
        }
        Answer(sum);
    }

    private static Match LastMatchOf(string input, Regex re)
    {
        for (int i = input.Length - 1; i >= 0; i--)
        {
            var match = re.Match(input, i);
            if (match.Success)
                return match;
        }

        throw new Exception("Match not found");
    }
}
