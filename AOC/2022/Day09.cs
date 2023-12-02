namespace AOC._2022;

internal class Day09 : DayBase
{
    public override void Part1()
    {
        var head = new Knot();
        var tail = new Knot();
        head.Next = tail;
        var touchedPositions = new List<string> { tail.ToString() };
        tail.OnMoved += _ =>
        {
            var name = tail.ToString();
            if (!touchedPositions.Contains(name))
                touchedPositions.Add(name);
        };
        var lines = GetInputLines();
        foreach (var line in lines)
        {
            var steps = int.Parse(line.Substring(2));
            for (var step = 1; step <= steps; step++)
            {
                switch (line[0])
                {
                    case 'U':
                        head.Y++;
                        break;
                    case 'D':
                        head.Y--;
                        break;
                    case 'R':
                        head.X++;
                        break;
                    case 'L':
                        head.X--;
                        break;
                }
            }
        }

        Answer(touchedPositions.Count);
    }

    public override void Part2()
    {
        Knot next, tail = null;
        var head = next = new Knot { Name = "H" };
        for (int i = 1; i < 10; i++)
            next = next.Next = tail = new Knot { Name = i.ToString() };
            
        var touchedPositions = new List<string> { tail.ToString() };
        var tailMoved = false;
        tail.OnMoved += _ => tailMoved = true;

        var lines = GetInputLines();
        for (int i = 0; i < lines.Length; i++)
        {
            var steps = int.Parse(lines[i].Substring(2));
            for (var step = 1; step <= steps; step++)
            {
                switch (lines[i][0])
                {
                    case 'U':
                        head.Y++;
                        break;
                    case 'D':
                        head.Y--;
                        break;
                    case 'R':
                        head.X++;
                        break;
                    case 'L':
                        head.X--;
                        break;
                    default:
                        throw new Exception("Unsupported instruction");
                }

                if (tailMoved)
                {
                    var name = tail.ToString();
                    if (!touchedPositions.Contains(name))
                        touchedPositions.Add(name);
                    tailMoved = false;
                }
            }
        }

        Answer(touchedPositions.Count);
    }

    private class Knot : IDisposable
    {
        public string Name;
        private int y;
        private int x;

        public Knot Next;

        public event Action<Knot> OnMoved;

        public int Y
        {
            get => y; 
            set
            {
                if (y == value) return;
                y = value;
                OnMoved?.Invoke(this);
                Next?.Follow(this);
            }
        }

        public int X
        {
            get => x; 
            set
            {
                if (x == value) return;
                x = value;
                OnMoved?.Invoke(this);
                Next?.Follow(this);
            }
        }

        public void SetPos(int x, int y)
        {
            if(this.x == x && this.y == y) return;
            this.x = x;
            this.y = y;
            OnMoved?.Invoke(this);
            Next?.Follow(this);
        }

        private bool Follow(Knot leader)
        {
            var diff = new Point(leader.X - X, leader.Y - Y);
            if (diff.X <= 1 && diff.X >= -1 && diff.Y <= 1 && diff.Y >= -1)
                return false;

            if (diff.X == 2)
            {
                if (diff.Y == 0)
                    X++;
                else if (diff.Y > 0)
                    SetPos(X + 1, Y + 1);
                else if (diff.Y < 0)
                    SetPos(X + 1, Y - 1);

                return true;
            }
            else if (diff.X == -2) 
            {
                if (diff.Y == 0)
                    X--;
                else if (diff.Y > 0)
                    SetPos(X - 1, Y + 1);
                else if (diff.Y < 0)
                    SetPos(X - 1, Y - 1);

                return true;
            }
            else if (diff.Y == 2) 
            {
                if (diff.X == 0)
                    Y++;
                else if (diff.X > 0)
                    SetPos(X + 1, Y + 1);
                else if (diff.X < 0)
                    SetPos(X - 1, Y + 1);

                return true;
            }
            else if (diff.Y == -2) 
            {
                if (diff.X == 0)
                    Y--;
                else if (diff.X > 0)
                    SetPos(X + 1, Y - 1);
                else if (diff.X < 0)
                    SetPos(X - 1, Y - 1);

                return true;
            }

            throw new Exception("Huh?!");
        }

        public override string ToString() => $"{Name}: {X} {Y}";

        public void Dispose()
        {
            Next?.Dispose();
            Next = null;
        }
    }
}
