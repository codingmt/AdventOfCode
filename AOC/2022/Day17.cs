namespace AOC._2022;

internal class Day17 : DayBase
{
    public override void Part1()
    {
        var shapes = new[]
        {
            new Shape(new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(5, 0)),
            new Shape(new Point(3, 0), new Point(2, 1), new Point(3, 1), new Point(4, 1), new Point(3, 2)),
            new Shape(new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(4, 1), new Point(4, 2)),
            new Shape(new Point(2, 0), new Point(2, 1), new Point(2, 2), new Point(2, 3)),
            new Shape(new Point(2, 0), new Point(3, 0), new Point(2, 1), new Point(3, 1))
        };
        var moves = GetInput();
        var movespos = -1;
        var field = new Shape(new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(5, 0), new Point(6, 0));
        for (int i = 0; i < 2022; i++)
        {
            var shape = shapes[i % 5];
            shape += field.Top + 4;
            while (true)
            {
                movespos = ++movespos % moves.Length;
                Shape shapemoved = null;
                switch (moves[movespos])
                {
                    case '<':
                        if (shape.Left > 0)
                            shapemoved = shape << 1;
                        break;
                    case '>':
                        if (shape.Right < 6)
                            shapemoved = shape >> 1;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                if (shapemoved != null && (shapemoved & field) == Shape.Empty)
                    shape = shapemoved;

                var shapedown = shape - 1;
                if ((shapedown & field) != Shape.Empty)
                    break;

                shape = shapedown;
            }

            field |= shape;
        }

        Answer(field.Top);
    }

    public override void Part2()
    {
    }

    private class Shape
    {
        public Point[] Points;
        public readonly int Width;
        public readonly int Height;
        public int Top;
        public int Left;
        public int Right => Left + Width - 1;
        public int Botton => Top - Height + 1;

        public Shape(params Point[] points)
        {
            Points = points;
            Left = points.Min(p => p.X);
            Top = points.Max(p => p.Y);
            Width = points.Max(p => p.X) - Left + 1;
            Height = Top - points.Min(p => p.Y) + 1;
        }

        private Shape(Point[] points, int left, int top, int width, int height)
        {
            Points = points;
            Left = left;
            Top = top;
            Height = height;
            Width = width;
        }

        public Shape Copy() => new(Points, Left, Top, Width, Height);

        public static Shape operator >>(Shape shape, int steps) =>
            new(shape.Points.Select(p => new Point(p.X + steps, p.Y)).ToArray(), shape.Left + steps, shape.Top, shape.Width, shape.Height);

        public static Shape operator <<(Shape shape, int steps) => shape >> -steps;

        public static Shape operator +(Shape shape, int steps) =>
            new(shape.Points.Select(p => new Point(p.X, p.Y + steps)).ToArray(), shape.Left, shape.Top + steps, shape.Width, shape.Height);

        public static Shape operator -(Shape shape, int steps) => shape + -steps;

        public static Shape operator |(Shape left, Shape right) =>
            new(left.Points.Union(right.Points).ToArray());

        public static Shape operator &(Shape left, Shape right)
        {
            var points = left.Points.Intersect(right.Points).ToArray();
            return points.Any() ? new Shape(points) : Shape.Empty;
        }

        public static bool operator ==(Shape left, Shape right)
        {
            if (left is null && right is null)
                return true;
            if (left is null ^ right is null)
                return false;

            return left.Points.Length == right.Points.Length &&
                left.Points.Select((p, i) => p == right.Points[i]).All(x => x);
        }

        public static bool operator !=(Shape left, Shape right) => !(left == right);

        public static Shape Empty => new(Array.Empty<Point>(), 0, 0, 0, 0);
    }
}
