namespace AOC
{
    internal class Day06 : DayBase
    {
        public override void Part1()
        {
            var input = GetInput();
            int i;
            for (i = 4; i < input.Length; i++)
            {
                var prev = input.Substring(i - 4, 4).ToArray().OrderBy(x => x).ToArray();
                if (prev[0] != prev[1] && prev[1] != prev[2] && prev[2] != prev[3])
                    break;
            }
            Answer(i);
        }

        public override void Part2()
        {
            var input = GetInput();
            int i;
            for (i = 14; i < input.Length; i++)
            {
                var prev = input.Substring(i - 14, 14).ToArray().OrderBy(x => x).ToArray();
                int j;
                for (j = 1; j < 14; j++)
                    if (prev[j - 1] == prev[j])
                        break;
                if (j == 14)
                    break;
            }
            Answer(i);
        }
    }
}
