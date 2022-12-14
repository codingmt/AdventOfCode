using System.Drawing.Drawing2D;
using System.Text;

namespace AOC
{
    internal class Day12 : DayBase
    {
        private string testinput1 = "Saa\r\nbcc\r\nbcE";

        private string testinput = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";

        private string input = @"abaaaaacccccccccccccccccccccccccccccccccccccccaaaaaaaccccaaaaaaaaaaaaaaaaacccccaaaaaacccccccccccccccccccccccaaaaaaaaccccccccccccccccccccccccccccccccaaaaaa
abaaaaaacccaaaacccccccccccccccccccccccaccccccccaaaaaaaaccaaaaaaaaaaaaaaaaccccccaaaaaacccccccccccccccccccccccccaaaaccccccccccccccccccccccccccccccccccaaaaaa
abaaaaaacccaaaacccccccccccccccccaaaaaaaacccccccaaaaaaaaacaaaaaaaaaaaaacccccccccaaaaacccccccccccccccccccccccccaaaaacccccccccccccccccccaaaccccccccccccaaaaaa
abaaacaccccaaaaccccccccccccccccccaaaaaacccccccccaaaaaaaccccaaaaaaaaaaacccccccccaaaaacccccccccccccccccccccccccaacaaaccccccccccccccccccaaacccccccccccccccaaa
abaaacccccccaaacccccccccccaacccccaaaaaaccccccccaaaaaaccccccaacaaaaaaaacccccccccccccccccccccccaaccccccccccccccacccaaaaacccccccccaaccccaaacccccccccccccccaaa
abccccccccccccccccccccccccaaaaccaaaaaaaacccccccaaaaaaaccccccccaaaaaaaaaccccccccccaacccccccccaaaccccccccccccccccccacaaacccccccccaaaaccaaacccccccccccccccaac
abccccccccccccccccccccccaaaaaacaaaaaaaaaaccccccaaccaaaaacccccaaaaccaaaaccccccccccaaacaacccccaaacaaacccaaccccccccaaaaaaaacccccccaaaaakkkkkkcccccccccccccccc
abccccccccccccccccccccccaaaaaccaaaaaaaaaacccccccccccaaaaaaccccacccaaaaaccccccccccaaaaaaccaaaaaaaaaaaaaaaccccccccaaaaaaaaccccccccaaajkkkkkkkaccccccaacccccc
abcccccccccccccccccccccccaaaaacacacaaaccccccccccccccaaaaaaccccccccaaaacccccccccaaaaaaacccaaaaaaaaaaaaaaaaaccccccccaaaaaccccccccccjjjkkkkkkkkccaaaaaacccccc
abcccccccccccccccccccccccaacaacccccaaacccaccccccccccaaaaaaccccccccaaaacccccccccaaaaaaacccccaaaaaacaaaaaaaacccccccaaaaacccccccjjjjjjjooopppkkkcaaaaaaaccccc
abcccccccccccccccccaacaacccccccccccaaaaaaacccccccccccaaaaacccccccccccccccccccccccaaaaaaccccaaaaaaccaaaaaaacccccccaaaaaacciijjjjjjjjoooopppkkkcaaaaaaaacccc
abccccccccccaaaccccaaaaacccccccccccccaaaaacccccccccccaaaaccccccccccccccccccccccccaacaaaccccaaaaaaacaaaaacccccccccaccaaaciiiijjjjjjoooopppppkllcaaaaaaacccc
abccaaccccccaaaaacaaaaacccccccccccccaaaaaacccccccccccccccccccccccccccccccccccccccaacccccccaaaacaaaaaaaaacccaaccccaaaaaciiiiinoooooooouuuupplllaaaaaacccccc
abcaaacccccaaaaaacaaaaaacccccccccccaaaaaaaaccccccccaacaccccccccccccccccccccccccccccccccccccaccccccccccaaccaaaccccaaaaaciiinnnooooooouuuuuppplllaaacacccccc
abaaaaaacccaaaaaacccaaaacccccccccccaaaaaaaaccccccccaaaaccccccccccccccccccccccccccccccccccccccccccccccccaaaaacaacaaaaaaiiinnnnntttoouuuuuupppllllcccccccccc
abaaaaaaccccaaaaacccaaccccccccccacccccaaccccccccccaaaaaccccccccccccccccccccccccccccccccccccccccccccccccaaaaaaaacaaaaaaiiinnnnttttuuuuxxuuupppllllccccccccc
abaaaaacccccaacaaccccccccccccccaaaccccaacccccaacccaaaaaacccccccccccccccccccccccccccccccccccccccccccccccccaaaaaccaaaaaaiiinnnttttxxuuxxyyuuppppllllcccccccc
abaaaacccccccccccccccccccccaaacaaaccccccaaacaaaaccacaaaacccccccccccccccccccccccccccccccccccaacccccccccccaaaaaccccaaaccciinnntttxxxxxxxyyvvvqqqqqlllccccccc
abaaaaaccccccccccccccccccccaaaaaaaaaacccaaaaaaacccccaaccccccccccccccccccccccccccccccccccccaaacccccccccccaacaaaccccccccciiinntttxxxxxxxyyvvvvvqqqqljjcccccc
abccaaaccaccccccccaaacccccccaaaaaaaaaccccaaaaaacccccccccccccccccccccccccccccccaacccccccaaaaacaaccccccccccccaacccccccccchhinnnttxxxxxxyyyyyvvvvqqqjjjcccccc
SbccccaaaacccccccaaaaaacccccccaaaaaccccccaaaaaaaaccccccccccccccccccccaaccccccaaaaccccccaaaaaaaacccccccccccccccccccccccchhhnnntttxxxxEzyyyyyvvvqqqjjjcccccc
abccccaaaacccccccaaaaaaccccccaaaaaacccccaaaaaaaaaacccccccccccccccccccaaccccccaaaaccccccccaaaaacccccccccccccccccccccccccchhhnntttxxxyyyyyyyvvvvqqqjjjcccccc
abcccaaaaaaccccccaaaaaacccccaaaaaaaccccaaaaaaaaaacccccccccccccccccaaaaaaaacccaaaacccccccaaaaaccccccccccccccccccccccccccchhmmmttxxxyyyyyyvvvvvqqqjjjdcccccc
abcccaaaaaacccccccaaaaacccccaaacaaacaaaaaaaaaaccccccccccccaaacccccaaaaaaaaccccccccccccccaacaaacccccccaacaaacccccccccccchhhmmmtswwwyyyyyyvvvqqqqjjjjdddcccc
abcccccaacccccccccaacaacccccccccccacaaaaaccaaaccccccccccaaaaacccccccaaaacccccccccccccccccccaaccccccccaaaaaacccccccccccchhhmmssswwwwwwyyywvrqqqjjjjdddccccc
abcccccccccccccccccccccccccccccccccaaaaaccccaaccccccccacaaaaaacccccaaaaacccccccccccccccccccccccccccccaaaaaacccccccccccchhhmmssswwwwwwywywwrrqjjjjddddccccc
abcccccccccccccccccccccccccccccccccaaaaaccccccccaaacaaacaaaaaacccccaaaaaaccccccccccccccccccccccccccccaaaaaaaccccccccccchhmmmsssswwsswwwwwwrrkkjjddddcccccc
abccccccccccccccccccccccccccccccccccaaaaacccccccaaaaaaacaaaaaccccccaaccaacccccccccccaaccccccccccccccaaaaaaaacaacaaccccchhhmmmsssssssswwwwrrrkkjddddaaccccc
abcccccccccccccccccccccccccaaaaaccccaacccccccccccaaaaaacaaaaacccccccccccccaacccccccaaaaaacccccccccccaaaaaaaacaaaaaccccchhgmmmmssssssrrwwwrrrkkddddaaaccccc
abcccccccccccccccccccccccccaaaaacccccccccccccccccaaaaaaaacccccccccccccccaaaaaaccccccaaaaaccccaaccccccccaaacccaaaaaaccccgggmmmmmmllllrrrrrrrkkkeedaaaaccccc
abcccccccccccaaccccccccccccaaaaaacccccccccccccccaaaaaaaaacccccccccccccccaaaaaaccccaaaaaaacccaaaacccccccaaccccaaaaaaccccggggmmmmllllllrrrrrkkkkeedaaaaacccc
abcccccccccccaaacaacaaaccccaaaaaaccccccccccccccaaaaaaaaaacccccccccccccccaaaaaaccccaaaaaaaaccaaaacccccccccccccaaaaaccccccgggggglllllllllrrkkkkeeeaaaaaacccc
abcccccccccccaaaaaacaaaacccaaaaaaccccccccccccccaaacaaaaaaccccccccccccccccaaaaaccccaaaaaaaaccaaaacccccccccccaaccaaaccccccgggggggggffflllkkkkkkeeeaaaaaacccc
abaccccccccaaaaaaaccaaaacccccaaacccccccccccccccccccaaaaaacaccccccccaaccccaaaacccccccaaacacccccccccccccccaaaaaccccccccccccccgggggffffflllkkkkeeeccaaacccccc
abaccccccccaaaaaaaccaaacccccccccccccccccaaaccccccccaaacaaaaaccccccaaacccccccccccccaaaacccccccccccccccccccaaaaaccccccccccccccccccaffffffkkkeeeeeccaaccccccc
abaaaccccccccaaaaaaccccccccccccccccccccaaaaaacccccccaaaaaaaacaaaacaaacccccccccaaaaaacccccccccccccccccccccaaaaaccccccccccccccccccccaffffffeeeeecccccccccccc
abaacccccccccaacaaaccccccccccccccccccccaaaaaaccccccccaaaaaccaaaaaaaaacccccccccaaaaaaaaccccccccccaaccccccaaaaacccccccccccccccccccccaaaffffeeeecccccccccccaa
abaacccccccccaaccccccccccccccccaaccccccaaaaacaaccaacccaaaaacaaaaaaaaacccccccccaaaaaaaaccccccaaacaacccccccccaacccccccccccccccccccccaaaccceaecccccccccccccaa
abaacccccccccccccccccccccccccccaaaaaacccaaaaaaaaaaaccaaacaaccaaaaaaaaaaaaacccccaaaaaaacccccccaaaaaccccccccccccccccccccccccccccccccaaacccccccccccccccaaacaa
abcccccccccccccccccccccccccccccaaaaaccccaacaacaaaaacccaaccccccaaaaaaaaaaaacccccaaaaacccccccccaaaaaaaccccccccccccccccccccccccccccccaaacccccccccccccccaaaaaa
abcccccccccccccccccccccccccccaaaaaaaccccccccaaaaaaaaccccccccccaaaaaaaaaaccccccaaaaaaccccccccaaaaaaaaccccccccccccccccccccccccccccccccccccccccccccccccaaaaaa";

        public override void Run1()
        {
            /*// Attempt 1 - fail due to inefficiency
            var paths = GetPaths();
            Answer(paths.Min(x => x.Count) - 1);
            */

            /*// Attempt 2: use Grafen method - fail due to inefficiency
            var field = input.Split("\r\n");
            var rowlength = field[0].Length;

            var startpos = 0;
            var endpos = 0;
            for (int row = 0; row < field.Length; row++)
            {
                var pos = field[row].IndexOf('S');
                if (pos != -1)
                {
                    startpos = row * rowlength + pos;
                    field[row] = field[row].Replace('S', 'a');
                }

                pos = field[row].IndexOf('E');
                if (pos != -1)
                {
                    endpos = row * rowlength + pos;
                    field[row] = field[row].Replace('E', (char)('z' + 1));
                }
            }

            var characters = field.Sum(x => x.Length);
            var matrix = new Matrix(characters, characters);
            for (int row = 0; row < field.Length; row++)
                for (int col = 0; col < rowlength; col++)
                {
                    // can go up?
                    if (row > 0 &&
                        (field[row][col] == field[row - 1][col] ||
                        field[row][col] + 1 == field[row - 1][col]))
                        matrix[row * rowlength + col, (row - 1) * rowlength + col] = 1;

                    // can go down?
                    if (row < field.Length - 1 &&
                        (field[row][col] == field[row + 1][col] ||
                        field[row][col] + 1 == field[row + 1][col]))
                        matrix[row * rowlength + col, (row + 1) * rowlength + col] = 1;

                    // can go left?
                    if (col > 0 &&
                        (field[row][col] == field[row][col - 1] ||
                        field[row][col] + 1 == field[row][col - 1]))
                        matrix[row * rowlength + col, row * rowlength + col - 1] = 1;

                    // can go right?
                    if (col < rowlength - 1 &&
                        (field[row][col] == field[row][col + 1] ||
                        field[row][col] + 1 == field[row][col + 1]))
                        matrix[row * rowlength + col, row * rowlength + col + 1] = 1;
                }

            var factor = matrix.Copy();

            //matrix = new int[3, 3] { { 1, 2, 1 }, { 2, 0, 2 }, { 3, 4, 0 } };
            //characters = 3;

            //Console.WriteLine(matrix.ToString());
            //Console.WriteLine();

            
            int steps;
            for (steps = 1; matrix[startpos, endpos] == 0; steps++)
            {
                matrix ^= factor;

                //Console.WriteLine(matrix.ToString());
                //Console.WriteLine();
            }

            Answer(steps);
            */

            // Attempt 3: Dijkstra algorithm
            var field = input.Split("\r\n");
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

            Answer(sptSet[endpos].Distance);
        }

        public override void Run2()
        {
        }

        public List<List<Point>> GetPaths()
        {
            var field = input.Split("\r\n").Select(x => x.ToArray()).ToArray();
            var paths = new List<List<Point>> { };
            extendPath(new List<Point> { new Point(0, 20) });
            return paths;

            void extendPath(List<Point> path)
            {
                var last = path.Last();
                var current = field[last.Y][last.X];
                if (current == 'S')
                    current = 'a';
                if (current == 'E')
                {
                    paths.Add(path);
                    return;
                }
                var up = current == 'z' ? 'E' : (char)(current + 1);
                var down = current == 'a' ? current : (char)(current - 1);

                // up? (Y - 1)
                if (last.Y > 0 &&
                    !pointInRoute(last.X, last.Y - 1) &&
                    validChar(field[last.Y - 1][last.X]))
                    continuePath(path, new Point(last.X, last.Y - 1));

                // down? (Y + 1)
                if (last.Y < field.Length - 1 &&
                    !pointInRoute(last.X, last.Y + 1) &&
                    validChar(field[last.Y + 1][last.X]))
                    continuePath(path, new Point(last.X, last.Y + 1));

                // left? (X - 1)
                if (last.X > 0 &&
                    !pointInRoute(last.X - 1, last.Y) &&
                    validChar(field[last.Y][last.X - 1]))
                    continuePath(path, new Point(last.X - 1, last.Y));

                // right? (X + 1)
                if (last.X < field[0].Length - 1 &&
                    !pointInRoute(last.X + 1, last.Y) &&
                    validChar(field[last.Y][last.X + 1]))
                    continuePath(path, new Point(last.X + 1, last.Y));

                bool pointInRoute(int x, int y) => path.Contains(new Point(x, y));
                bool validChar(char c) => c == current || c == up /*|| c == down*/;
                void continuePath(List<Point> path, Point newPoint)
                {
                    path = path.ToList();
                    path.Add(newPoint);
                    extendPath(path);
                }
            }
        }

        private class Matrix
        {
            private readonly int[,] m;

            public int Width => m.GetLength(0);

            public int Height => m.GetLength(1);

            public Matrix(int width, int height)
            {
                m = new int[width, height];
            }

            public int this[int x, int y]
            {
                get => m[x, y];
                set => m[x, y] = value;
            }

            public Matrix Copy()
            {
                var result = new Matrix(Width, Height);

                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                        result[x, y] = m[x, y];

                return result;
            }

            public static Matrix operator *(Matrix left, Matrix right)
            {
                var parts = left.Width;
                if (parts != right.Height)
                    throw new ArgumentException("Matrix dimensions do not match.");

                var width = right.Width;
                var height = left.Height;
                var result = new Matrix(width, height);

                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        var value = 0;

                        for (int source = 0; source < parts; source++)
                            value += left[source, y] * right[x, source];

                        result[x, y] = value;
                    }

                return result;
            }

            public static Matrix operator ^(Matrix left, Matrix right)
            {
                var parts = left.Width;
                if (parts != right.Height)
                    throw new ArgumentException("Matrix dimensions do not match.");

                var width = right.Width;
                var height = left.Height;
                var result = new Matrix(width, height);

                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        for (int source = 0; source < parts; source++)
                            if (left[source, y] != 0 && right[x, source] != 0)
                            {
                                result[x, y] = 1;
                                break;
                            }
                    }

                return result;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (int y = 0; y < Height; y++)
                { 
                    for (int x = 0; x < Width; x++)
                        sb.Append($"{m[x, y]}\t");
                    sb.AppendLine();
                }
                return sb.ToString();
            }
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
