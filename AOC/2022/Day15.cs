namespace AOC._2022;

internal class Day15 : DayBase
{
    public override void Part1()
    {
        var sensors = GetInputLines().Select(x => new Sensor(x)).ToArray();
        var covered = new HashSet<int>();
        const int y = 2_000_000;
        foreach (var sensor in sensors.Where(s => s.TouchesY(y)))
            foreach (var x in sensor.GetXPositions(y))
                covered.Add(x);
        Answer(covered.Count);
    }

    public override void Part2()
    {
        var sensors = GetInputLines().Select(x => new Sensor(x)).ToArray();
        const int maxcoord = 4_000_000;
        var lines = sensors.SelectMany(x => x.GetLines()).ToArray();
        var ascLines = lines.Where(x => x.Ascends).ToArray();
        var descLines = lines.Where(x => !x.Ascends).ToArray();
        var intersections = ascLines
            .SelectMany(l1 => descLines.Select(l2 => l1 * l2))
            .Where(p => p != null && p.X >= 0 && p.X <= maxcoord && p.Y >= 0 && p.Y <= maxcoord)
            .OrderBy(p => p.Y)
            .ThenBy(p => p.X)
            .GroupBy(p => $"{p.X} {p.Y} {p.OpenSide}")
            .ToDictionary(g => g.Key, g => g.First());

        foreach (var intersection in intersections.Values.Where(p => p.OpenSide == Dir.B))
        {
            if (intersections.ContainsKey($"{intersection.X - 1} {intersection.Y + 1} R") &&
                intersections.ContainsKey($"{intersection.X + 1} {intersection.Y + 1} L") &&
                intersections.ContainsKey($"{intersection.X} {intersection.Y + 2} T"))
            {
                Answer(intersection.X * 4_000_000L + intersection.Y + 1);
            }
        }
    }

    private class Sensor
    {
        public readonly Point OwnPosition;
        public readonly Point Beacon;
        public readonly int ManhattanDistance;

        public Sensor(string line)
        {
            var parts = line.Split(new [] { "Sensor at x=", ", y=", ": closest beacon is at x=", ", y=" }, StringSplitOptions.RemoveEmptyEntries);
            OwnPosition = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
            Beacon = new Point(int.Parse(parts[2]), int.Parse(parts[3]));
            ManhattanDistance = Math.Abs(Beacon.X - OwnPosition.X) + Math.Abs(Beacon.Y - OwnPosition.Y);
        }

        public bool TouchesY(int y) => y >= OwnPosition.Y - ManhattanDistance && y <= OwnPosition.Y + ManhattanDistance;

        public List<int> GetXPositions(int y)
        {
            var result = new List<int>();
            var deltay = Math.Abs(y - OwnPosition.Y);
            var fromx = OwnPosition.X - ManhattanDistance + deltay;
            var tox = OwnPosition.X + ManhattanDistance - deltay;
            for (int x = fromx; x < tox; x++)
                result.Add(x);
            return result;
        }

        public IEnumerable<Line> GetLines()
        {
            var points = new[]
            {
                new Point(OwnPosition.X, OwnPosition.Y - ManhattanDistance),
                new Point(OwnPosition.X + ManhattanDistance, OwnPosition.Y),
                new Point(OwnPosition.X, OwnPosition.Y + ManhattanDistance),
                new Point(OwnPosition.X - ManhattanDistance, OwnPosition.Y),
            };
            yield return new Line(points[0], points[1], DiagDir.RT);
            yield return new Line(points[1], points[2], DiagDir.RB);
            yield return new Line(points[2], points[3], DiagDir.LB);
            yield return new Line(points[3], points[0], DiagDir.LT);
        }
    }

    private enum Dir
    {
        L, R, T, B
    }

    private enum DiagDir
    {
        LT, LB, RB, RT
    }

    private class PointWithOpenSide
    {
        public int X;
        public int Y;
        public Dir OpenSide;

        public PointWithOpenSide(int x, int y, Dir openSide)
        {
            X = x;
            Y = y;
            OpenSide = openSide;
        }
    }

    private class Line
    {
        public Point From;
        public Point To;
        public DiagDir Outside;
        public bool Ascends;

        public Line(Point from, Point to, DiagDir outside)
        {
            From = from.X < to.X ? from : to;
            To = from.X < to.X ? to : from;
            Outside = outside;
            Ascends = To.Y > From.Y;
        }

        public static PointWithOpenSide operator *(Line left, Line right)
        {
            if (left == right || left.Ascends == right.Ascends) return null;

            // y = ax + b
            var aLeft = (left.To.Y - left.From.Y) / (left.To.X - left.From.X);
            var bLeft = left.To.Y - aLeft * left.To.X;
            var aRight = (right.To.Y - right.From.Y) / (right.To.X - right.From.X);
            var bRight = right.To.Y - aRight * right.To.X;

            // aLeft * x + bLeft = aRight * x + bRight
            // aLeft * x - aRight * x = bRight - bLeft
            // (aLeft - aRight) * x = bRight - bLeft
            // x = (bRight - bLeft) / (aLeft - aRight)
            var x = (bRight - bLeft) / (aLeft - aRight);
            if (x <= left.From.X || x >= left.To.X || x <= right.From.X || x >= right.To.X)
                return null;

            var y = aLeft * x + bLeft;
            return new PointWithOpenSide(x, y, GetDir(left.Outside, right.Outside));

            static Dir GetDir(DiagDir left, DiagDir right)
            {
                switch (left)
                {
                    case DiagDir.LT:
                        switch (right)
                        {
                            case DiagDir.LB:
                                return Dir.L;
                            case DiagDir.RT:
                                return Dir.T;
                            default:
                                throw new NotImplementedException();
                        }
                    case DiagDir.LB:
                        switch (right)
                        {
                            case DiagDir.LT:
                                return Dir.L;
                            case DiagDir.RB:
                                return Dir.B;
                            default:
                                throw new NotImplementedException();
                        }
                    case DiagDir.RB:
                        switch (right)
                        {
                            case DiagDir.LB:
                                return Dir.B;
                            case DiagDir.RT:
                                return Dir.R;
                            default:
                                throw new NotImplementedException();
                        }
                    case DiagDir.RT:
                        switch (right)
                        {
                            case DiagDir.LT:
                                return Dir.T;
                            case DiagDir.RB:
                                return Dir.R;
                            default:
                                throw new NotImplementedException();
                        }
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
