namespace AOC
{
    internal class Day08 : DayBase
    {
        public override void Part1()
        {
            var grid = GetInputLines();
            var visible = new bool[grid.Length][];
            char maxHeight;

            for (int row = 0; row < grid.Length; row++)
                visible[row] = new bool[grid[0].Length];

            // look from the top
            for (int col = 0; col < grid[0].Length; col++)
            {
                maxHeight = '/';
                for (int row = 0; row < grid.Length; row++)
                {
                    if (grid[row][col] > maxHeight)
                    {
                        visible[row][col] |= true;
                        maxHeight = grid[row][col];
                        if (maxHeight == '9')
                            break;
                    }
                }
            }

            // look from the top
            for (int col = 0; col < grid[0].Length; col++)
            {
                maxHeight = '/';
                for (int row = grid.Length - 1; row >= 0; row--)
                {
                    if (grid[row][col] > maxHeight)
                    {
                        visible[row][col] |= true;
                        maxHeight = grid[row][col];
                        if (maxHeight == '9')
                            break;
                    }
                }
            }

            // look from the left
            for (int row = 0; row < grid.Length; row++)
            {
                maxHeight = '/';
                for (int col = 0; col < grid[0].Length; col++)
                {
                    if (grid[row][col] > maxHeight)
                    {
                        visible[row][col] |= true;
                        maxHeight = grid[row][col];
                        if (maxHeight == '9')
                            break;
                    }
                }
            }

            // look from the right
            for (int row = 0; row < grid.Length; row++)
            {
                maxHeight = '/';
                for (int col = grid[0].Length - 1; col >= 0; col--)
                {
                    if (grid[row][col] > maxHeight)
                    {
                        visible[row][col] |= true;
                        maxHeight = grid[row][col];
                        if (maxHeight == '9')
                            break;
                    }
                }
            }

            Answer(visible.Sum(row => row.Count(x => x)));
        }

        public override void Part2()
        {
            var grid = GetInputLines();
            var scenicScores = new int[grid.Length][];
            for (int row = 0; row < grid.Length; row++)
                scenicScores[row] = new int[grid[0].Length];
            int up, down, left, right;

            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[0].Length; col++)
                {
                    for (up = 0; up < row; up++)
                        if (grid[row][col] <= grid[row - up - 1][col])
                        {
                            up++;
                            break;
                        }
                    for (down = 0; down < grid.Length - row - 1; down++)
                        if (grid[row][col] <= grid[row + down + 1][col])
                        {
                            down++;
                            break;
                        }
                    for (left = 0; left < col; left++)
                        if (grid[row][col] <= grid[row][col - left - 1])
                        {
                            left++;
                            break;
                        }
                    for (right = 0; right < grid[0].Length - col - 1; right++)
                        if (grid[row][col] <= grid[row][col + right + 1])
                        {
                            right++;
                            break;
                        }

                    scenicScores[row][col] = up * down * left * right;
                }
            }

            Answer(scenicScores.Max(row => row.Max()));
        }
    }
}
