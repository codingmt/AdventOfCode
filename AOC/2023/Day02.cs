namespace AOC._2023;

internal class Day02 : DayBase
{
    public override void Part1()
    {
        var lines = GetInputLines();
        var games = lines.Select(Game.Parse).ToArray();
        Answer(games
            .Where(g => !g.CubeSets.Any(s => s.Red > 12 || s.Green > 13 || s.Blue > 14))
            .Select(g => g.Id)
            .Sum());
    }

    public override void Part2()
    {
        var lines = GetInputLines();
        var games = lines.Select(Game.Parse).ToArray();
        Answer(games
            .Select(g => g.CubeSets.Max(s => s.Red) * g.CubeSets.Max(s => s.Green) * g.CubeSets.Max(s => s.Blue))
            .Sum());
    }

    private class Game
    {
        public int Id;
        public CubeSet[] CubeSets;

        public static Game Parse(string input)
        {
            var parts = input.Split(':', ';');
            return new Game
            {
                Id = int.Parse(parts[0][5..]),
                CubeSets = parts.Skip(1).Select(CubeSet.Parse).ToArray()
            };
        }
    }

    private class CubeSet
    {
        public int Red;
        public int Blue;
        public int Green;

        public static CubeSet Parse(string input)
        {
            var result = new CubeSet();
            var parts = input.Trim().Replace(",", "").Split(' ');
            for (int i = 0; i < parts.Length - 1; i+=2)
            {
                switch (parts[i + 1]) 
                {
                    case "red":
                        result.Red = int.Parse(parts[i]);
                        break;
                    case "blue":
                        result.Blue = int.Parse(parts[i]);
                        break;
                    case "green":
                        result.Green = int.Parse(parts[i]);
                        break;
                    default:
                        throw new Exception();
                }
            }
            return result;
        }
    }
}
