namespace AOC
{
    internal class Day12 : DayBase
    {
        public override void Part1()
        {
            // Dijkstra algorithm
            var field = GetInputLines();
            var rowlength = field[0].Length;
            var sptSet = new Dictionary<string, ShortestDistance>();

            // Find start and end positions
            var endpos = string.Empty;
            for (int row = 0; row < field.Length; row++)
            {
                var pos = field[row].IndexOf('S');
                if (pos >= 0)
                {
                    field[row] = field[row].Replace('S', 'a');
                    var start = new ShortestDistance(pos, row, 'S');
                    sptSet.Add(start.ToString(), start);
                }

                pos = field[row].IndexOf('E');
                if (pos >= 0)
                {
                    field[row] = field[row].Replace('E', (char)('z' + 1));
                    endpos = new ShortestDistance(pos, row, 'E').ToString();
                }
            }

            while (true)
            {
                var nearest = sptSet.Values.Where(x => !x.Closed).OrderBy(x => x.Distance).FirstOrDefault();
                if (nearest == null)
                    break;

                // can go up?
                if (nearest.Y > 0 &&
                    (field[nearest.Y][nearest.X] == field[nearest.Y - 1][nearest.X] ||
                    field[nearest.Y][nearest.X] + 1 == field[nearest.Y - 1][nearest.X] ||
                    field[nearest.Y][nearest.X] > field[nearest.Y - 1][nearest.X]))
                {
                    var add = new ShortestDistance(nearest.X, nearest.Y - 1, field[nearest.Y - 1][nearest.X], nearest);
                    var name = add.ToString();
                    if (!sptSet.ContainsKey(name))
                    {
                        sptSet.Add(name, add);
                        if (name == endpos)
                            break;
                    }
                }

                // can go down?
                if (nearest.Y < field.Length - 1 &&
                    (field[nearest.Y][nearest.X] == field[nearest.Y + 1][nearest.X] ||
                    field[nearest.Y][nearest.X] + 1 == field[nearest.Y + 1][nearest.X] ||
                    field[nearest.Y][nearest.X] > field[nearest.Y + 1][nearest.X]))
                {
                    var add = new ShortestDistance(nearest.X, nearest.Y + 1, field[nearest.Y + 1][nearest.X], nearest);
                    var name = add.ToString();
                    if (!sptSet.ContainsKey(name))
                    {
                        sptSet.Add(name, add);
                        if (name == endpos)
                            break;
                    }
                }

                // can go left?
                if (nearest.X > 0 &&
                    (field[nearest.Y][nearest.X] == field[nearest.Y][nearest.X - 1] ||
                    field[nearest.Y][nearest.X] + 1 == field[nearest.Y][nearest.X - 1] ||
                    field[nearest.Y][nearest.X] > field[nearest.Y][nearest.X - 1]))
                {
                    var add = new ShortestDistance(nearest.X - 1, nearest.Y, field[nearest.Y][nearest.X - 1], nearest);
                    var name = add.ToString();
                    if (!sptSet.ContainsKey(name))
                    {
                        sptSet.Add(name, add);
                        if (name == endpos)
                            break;
                    }
                }

                // can go right?
                if (nearest.X < rowlength - 1 &&
                    (field[nearest.Y][nearest.X] == field[nearest.Y][nearest.X + 1] ||
                    field[nearest.Y][nearest.X] + 1 == field[nearest.Y][nearest.X + 1] ||
                    field[nearest.Y][nearest.X] > field[nearest.Y][nearest.X + 1]))
                {
                    var add = new ShortestDistance(nearest.X + 1, nearest.Y, field[nearest.Y][nearest.X + 1], nearest);
                    var name = add.ToString();
                    if (!sptSet.ContainsKey(name))
                    {
                        sptSet.Add(name, add);
                        if (name == endpos)
                            break;
                    }
                }

                nearest.Closed = true;
            }

            var p = sptSet[endpos];
            do
            {
                field[p.Y] = field[p.Y].Substring(0, p.X) + $"{field[p.Y][p.X]}".ToUpper() + field[p.Y].Substring(p.X + 1);
                p = p.Parent;
            } while (p != null);

            for (int i = 0; i < field.Length; i++)
                Console.WriteLine(field[i]);
            Console.WriteLine();

            Answer(sptSet[endpos].Distance - 2); //Apparently the S and E tiles shouldn't be counted
        }

        public override void Part2()
        {
            // Dijkstra algorithm
            var field = GetInputLines();
            var rowlength = field[0].Length;
            var sptSet = new Dictionary<string, ShortestDistance>();

            // Find end position
            for (int row = 0; row < field.Length; row++)
            {
                var pos = field[row].IndexOf('E');
                if (pos >= 0)
                {
                    field[row] = field[row].Replace('E', (char)('z' + 1));
                    var end = new ShortestDistance(pos, row, (char)('z' + 1));
                    sptSet.Add(end.ToString(), end);
                }
            }

            ShortestDistance endpos = null;
            while (true)
            {
                var nearest = sptSet.Values.Where(x => !x.Closed).OrderBy(x => x.Distance).FirstOrDefault();
                if (nearest == null)
                    break;

                // can go up?
                if (nearest.Y > 0 &&
                    (field[nearest.Y][nearest.X] == field[nearest.Y - 1][nearest.X] ||
                    field[nearest.Y][nearest.X] - 1 == field[nearest.Y - 1][nearest.X] ||
                    field[nearest.Y][nearest.X] < field[nearest.Y - 1][nearest.X]))
                {
                    var add = new ShortestDistance(nearest.X, nearest.Y - 1, field[nearest.Y - 1][nearest.X], nearest);
                    var name = add.ToString();
                    if (!sptSet.ContainsKey(name))
                    {
                        sptSet.Add(name, add);
                        if (add.Elevation == 'a')
                        {
                            endpos = add;
                            break;
                        }
                    }
                }

                // can go down?
                if (nearest.Y < field.Length - 1 &&
                    (field[nearest.Y][nearest.X] == field[nearest.Y + 1][nearest.X] ||
                    field[nearest.Y][nearest.X] - 1 == field[nearest.Y + 1][nearest.X] ||
                    field[nearest.Y][nearest.X] < field[nearest.Y + 1][nearest.X]))
                {
                    var add = new ShortestDistance(nearest.X, nearest.Y + 1, field[nearest.Y + 1][nearest.X], nearest);
                    var name = add.ToString();
                    if (!sptSet.ContainsKey(name))
                    {
                        sptSet.Add(name, add);
                        if (add.Elevation == 'a')
                        {
                            endpos = add;
                            break;
                        }
                    }
                }

                // can go left?
                if (nearest.X > 0 &&
                    (field[nearest.Y][nearest.X] == field[nearest.Y][nearest.X - 1] ||
                    field[nearest.Y][nearest.X] - 1 == field[nearest.Y][nearest.X - 1] ||
                    field[nearest.Y][nearest.X] < field[nearest.Y][nearest.X - 1]))
                {
                    var add = new ShortestDistance(nearest.X - 1, nearest.Y, field[nearest.Y][nearest.X - 1], nearest);
                    var name = add.ToString();
                    if (!sptSet.ContainsKey(name))
                    {
                        sptSet.Add(name, add);
                        if (add.Elevation == 'a')
                        {
                            endpos = add;
                            break;
                        }
                    }
                }

                // can go right?
                if (nearest.X < rowlength - 1 &&
                    (field[nearest.Y][nearest.X] == field[nearest.Y][nearest.X + 1] ||
                    field[nearest.Y][nearest.X] - 1 == field[nearest.Y][nearest.X + 1] ||
                    field[nearest.Y][nearest.X] < field[nearest.Y][nearest.X + 1]))
                {
                    var add = new ShortestDistance(nearest.X + 1, nearest.Y, field[nearest.Y][nearest.X + 1], nearest);
                    var name = add.ToString();
                    if (!sptSet.ContainsKey(name))
                    {
                        sptSet.Add(name, add);
                        if (add.Elevation == 'a')
                        {
                            endpos = add;
                            break;
                        }
                    }
                }

                nearest.Closed = true;
            }

            var p = endpos;
            do
            {
                field[p.Y] = field[p.Y].Substring(0, p.X) + $"{field[p.Y][p.X]}".ToUpper() + field[p.Y].Substring(p.X + 1);
                p = p.Parent;
            } while (p != null);

            for (int i = 0; i < field.Length; i++)
                Console.WriteLine(field[i]);
            Console.WriteLine();

            Answer(endpos.Distance - 2); //Apparently the S and E tiles shouldn't be counted
        }

        private class ShortestDistance
        {
            public readonly int Y;
            public readonly int X;
            public readonly char Elevation;
            public readonly ShortestDistance Parent;
            public readonly int Distance;
            public bool Closed;

            public ShortestDistance(int x, int y, char elevation, ShortestDistance parent = null)
            {
                X = x;
                Y = y;
                Elevation = elevation;
                Parent = parent;
                Distance = (parent?.Distance ?? -1) + 1;
            }

            public string Path => $"{Parent?.Path}{Elevation}";

            public override string ToString() => $"{X} {Y}";
        }
    }
}
