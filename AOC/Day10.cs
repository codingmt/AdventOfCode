namespace AOC
{
    internal class Day10 : DayBase
    {
        public override void Part1()
        {
            var X = 1;
            var cycle = 0;
            var countDown = 20;
            var result = 0;

            var lines = GetInputLines();
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

            var lines = GetInputLines();
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
