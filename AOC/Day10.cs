namespace AOC
{
    internal class Day10 : DayBase
    {
        private string testinput = @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";

        private string input = @"noop
noop
noop
addx 6
addx -1
noop
addx 5
noop
noop
addx -12
addx 19
addx -1
noop
addx 4
addx -11
addx 16
noop
noop
addx 5
addx 3
addx -2
addx 4
noop
noop
noop
addx -37
noop
addx 3
addx 2
addx 5
addx 2
addx 10
addx -9
noop
addx 1
addx 4
addx 2
noop
addx 3
addx 2
addx 5
addx 2
addx 3
addx -2
addx 2
addx 5
addx -40
addx 25
addx -22
addx 2
addx 5
addx 2
addx 3
addx -2
noop
addx 23
addx -18
addx 2
noop
noop
addx 7
noop
noop
addx 5
noop
noop
noop
addx 1
addx 2
addx 5
addx -40
addx 3
addx 8
addx -4
addx 1
addx 4
noop
noop
noop
addx -8
noop
addx 16
addx 2
addx 4
addx 1
noop
addx -17
addx 18
addx 2
addx 5
addx 2
addx 1
addx -11
addx -27
addx 17
addx -10
addx 3
addx -2
addx 2
addx 7
noop
addx -2
noop
addx 3
addx 2
noop
addx 3
addx 2
noop
addx 3
addx 2
addx 5
addx 2
addx -5
addx -2
addx -30
addx 14
addx -7
addx 22
addx -21
addx 2
addx 6
addx 2
addx -1
noop
addx 8
addx -3
noop
addx 5
addx 1
addx 4
noop
addx 3
addx -2
addx 2
addx -11
noop
noop
noop";

        public override void Part1()
        {
            var X = 1;
            var cycle = 0;
            var countDown = 20;
            var result = 0;

            var lines = input.Split("\r\n");
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(' ');
                switch (parts[0])
                {
                    case "noop":
                        increaseCycle();
                        break;
                    case "addx":
                        increaseCycle();
                        increaseCycle();
                        X += int.Parse(parts[1]);
                        break;
                    default:
                        throw new Exception("Unknown instruction");
                }
            }

            Answer(result);

            void increaseCycle()
            {
                cycle++;
                countDown--;
                if (countDown == 0)
                {
                    result += X * cycle;
                    countDown = 40;
                }
            }
        }

        public override void Part2()
        {
            var X = 1;
            var cycle = 0;
            var pixels = new char[240];

            var lines = input.Split("\r\n");
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(' ');
                switch (parts[0])
                {
                    case "noop":
                        increaseCycle();
                        break;
                    case "addx":
                        increaseCycle();
                        increaseCycle();
                        X += int.Parse(parts[1]);
                        break;
                    default:
                        throw new Exception("Unknown instruction");
                }
            }

            var result = new string(pixels);
            for (int i = 0; i < 240; i += 40)
                Console.WriteLine(result.Substring(i, 40));

            void increaseCycle()
            {
                pixels[cycle] = (cycle % 40) >= X - 1 && (cycle % 40) <= X + 1 ? '#' : '.';

                cycle++;
            }
        }
    }
}
