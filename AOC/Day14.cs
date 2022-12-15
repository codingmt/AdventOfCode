namespace AOC
{
    internal class Day14 : DayBase
    {
        public override void Part1()
        {
            var occupied = new HashSet<string>();
            var lines = GetInputLines();
            var abyss = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { " -> ", "," }, StringSplitOptions.None);
                for (int i = 0; i < parts.Length - 2; i += 2)
                {
                    var fromx = int.Parse(parts[i]);
                    var fromy = int.Parse(parts[i + 1]);
                    var tox = int.Parse(parts[i + 2]);
                    var toy = int.Parse(parts[i + 3]);

                    if (fromx == tox)
                    {
                        if (fromy < toy)
                            for (int y = fromy; y <= toy; y++)
                                occupied.Add($"{fromx},{y}");
                        else
                            for (int y = fromy; y >= toy; y--)
                                occupied.Add($"{fromx},{y}");
                    }
                    else if (fromy == toy)
                    {
                        if (fromx < tox)
                            for (int x = fromx; x <= tox; x++)
                                occupied.Add($"{x},{fromy}");
                        else
                            for (int x = fromx; x >= tox; x--)
                                occupied.Add($"{x},{fromy}");
                    }
                    else
                        throw new NotImplementedException("Diagonal not implemented.");

                    if (fromy > abyss) 
                        abyss = fromy;
                    if (toy > abyss)
                        abyss = toy;
                }
            }

            var sandcounter = 0;
            while (true)
            {
                var sandx = 500;
                var sandy = 0;

                while (true)
                {
                    if (!occupied.Contains($"{sandx},{sandy + 1}"))
                        sandy++;
                    else if (!occupied.Contains($"{sandx - 1},{sandy + 1}"))
                    {
                        sandx--;
                        sandy++;
                    }
                    else if (!occupied.Contains($"{sandx + 1},{sandy + 1}"))
                    {
                        sandx++;
                        sandy++;
                    }
                    else
                    {
                        occupied.Add($"{sandx},{sandy}");
                        break;
                    }

                    if (sandy >= abyss)
                        break;
                }

                if (sandy >= abyss)
                    break;

                sandcounter++;
            }

            Answer(sandcounter);
        }

        public override void Part2()
        {
            var occupied = new HashSet<string>();
            var lines = GetInputLines();
            var floor = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { " -> ", "," }, StringSplitOptions.None);
                for (int i = 0; i < parts.Length - 2; i += 2)
                {
                    var fromx = int.Parse(parts[i]);
                    var fromy = int.Parse(parts[i + 1]);
                    var tox = int.Parse(parts[i + 2]);
                    var toy = int.Parse(parts[i + 3]);

                    if (fromx == tox)
                    {
                        if (fromy < toy)
                            for (int y = fromy; y <= toy; y++)
                                occupied.Add($"{fromx},{y}");
                        else
                            for (int y = fromy; y >= toy; y--)
                                occupied.Add($"{fromx},{y}");
                    }
                    else if (fromy == toy)
                    {
                        if (fromx < tox)
                            for (int x = fromx; x <= tox; x++)
                                occupied.Add($"{x},{fromy}");
                        else
                            for (int x = fromx; x >= tox; x--)
                                occupied.Add($"{x},{fromy}");
                    }
                    else
                        throw new NotImplementedException("Diagonal not implemented.");

                    if (fromy > floor)
                        floor = fromy;
                    if (toy > floor)
                        floor = toy;
                }
            }

            floor += 2;

            var sandcounter = 0;
            while (true)
            {
                var sandx = 500;
                var sandy = 0;
                sandcounter++;

                while (true)
                {
                    if (sandy == floor - 1)
                    {
                        occupied.Add($"{sandx},{sandy}");
                        break;
                    }

                    if (!occupied.Contains($"{sandx},{sandy + 1}"))
                        sandy++;
                    else if (!occupied.Contains($"{sandx - 1},{sandy + 1}"))
                    {
                        sandx--;
                        sandy++;
                    }
                    else if (!occupied.Contains($"{sandx + 1},{sandy + 1}"))
                    {
                        sandx++;
                        sandy++;
                    }
                    else
                    {
                        occupied.Add($"{sandx},{sandy}");
                        break;
                    }
                }

                if (sandy == 0)
                    break;
            }

            Answer(sandcounter);
        }
    }
}
