﻿namespace AOC
{
    internal class Day05 : DayBase
    {
        private string stacks = @"    [V] [G]             [H]        
[Z] [H] [Z]         [T] [S]        
[P] [D] [F]         [B] [V] [Q]    
[B] [M] [V] [N]     [F] [D] [N]    
[Q] [Q] [D] [F]     [Z] [Z] [P] [M]
[M] [Z] [R] [D] [Q] [V] [T] [F] [R]
[D] [L] [H] [G] [F] [Q] [M] [G] [W]
[N] [C] [Q] [H] [N] [D] [Q] [M] [B]";

        private string input = @"move 3 from 2 to 5
move 2 from 9 to 6
move 4 from 7 to 1
move 7 from 3 to 4
move 2 from 9 to 8
move 8 from 8 to 6
move 1 from 7 to 4
move 8 from 6 to 4
move 4 from 5 to 7
move 3 from 4 to 9
move 2 from 6 to 3
move 11 from 4 to 1
move 1 from 3 to 4
move 2 from 3 to 1
move 1 from 7 to 6
move 14 from 1 to 6
move 7 from 4 to 3
move 2 from 5 to 9
move 5 from 6 to 4
move 9 from 6 to 1
move 3 from 4 to 8
move 1 from 7 to 6
move 3 from 4 to 1
move 7 from 3 to 8
move 5 from 9 to 5
move 4 from 1 to 4
move 3 from 7 to 2
move 5 from 6 to 2
move 3 from 4 to 1
move 7 from 8 to 5
move 3 from 6 to 8
move 11 from 2 to 1
move 1 from 4 to 3
move 1 from 3 to 9
move 2 from 2 to 9
move 8 from 5 to 4
move 1 from 1 to 7
move 1 from 9 to 5
move 8 from 4 to 1
move 1 from 6 to 8
move 2 from 9 to 1
move 4 from 5 to 3
move 2 from 7 to 3
move 40 from 1 to 2
move 24 from 2 to 9
move 1 from 5 to 6
move 11 from 2 to 3
move 9 from 3 to 5
move 12 from 9 to 4
move 6 from 5 to 7
move 4 from 7 to 4
move 2 from 5 to 1
move 2 from 1 to 9
move 1 from 6 to 8
move 9 from 4 to 8
move 6 from 4 to 9
move 17 from 9 to 6
move 1 from 4 to 6
move 17 from 6 to 5
move 1 from 1 to 4
move 2 from 7 to 9
move 1 from 6 to 7
move 2 from 2 to 9
move 2 from 7 to 2
move 6 from 3 to 8
move 3 from 5 to 9
move 1 from 4 to 9
move 2 from 3 to 7
move 4 from 5 to 6
move 1 from 7 to 4
move 1 from 4 to 2
move 1 from 7 to 5
move 9 from 8 to 1
move 1 from 1 to 2
move 2 from 9 to 3
move 7 from 2 to 7
move 1 from 9 to 5
move 12 from 8 to 7
move 3 from 1 to 9
move 2 from 6 to 4
move 9 from 9 to 3
move 1 from 6 to 7
move 1 from 9 to 5
move 1 from 6 to 1
move 9 from 7 to 1
move 7 from 1 to 8
move 4 from 3 to 9
move 5 from 7 to 1
move 3 from 9 to 1
move 4 from 7 to 2
move 12 from 1 to 5
move 2 from 9 to 4
move 7 from 8 to 2
move 7 from 5 to 7
move 4 from 3 to 4
move 1 from 8 to 1
move 2 from 2 to 1
move 2 from 3 to 1
move 3 from 2 to 7
move 13 from 5 to 4
move 1 from 8 to 3
move 1 from 3 to 8
move 1 from 3 to 5
move 1 from 8 to 7
move 17 from 4 to 8
move 5 from 2 to 6
move 2 from 1 to 6
move 5 from 6 to 3
move 9 from 7 to 1
move 4 from 4 to 3
move 1 from 6 to 2
move 4 from 7 to 4
move 1 from 6 to 5
move 2 from 3 to 2
move 15 from 1 to 4
move 6 from 5 to 4
move 4 from 3 to 5
move 4 from 5 to 2
move 2 from 2 to 4
move 11 from 8 to 1
move 2 from 8 to 3
move 5 from 3 to 7
move 4 from 2 to 8
move 2 from 2 to 9
move 4 from 7 to 8
move 11 from 4 to 6
move 2 from 5 to 4
move 3 from 6 to 9
move 4 from 1 to 4
move 15 from 4 to 9
move 1 from 7 to 3
move 2 from 1 to 2
move 6 from 4 to 5
move 11 from 8 to 2
move 16 from 9 to 4
move 2 from 9 to 1
move 4 from 2 to 3
move 8 from 4 to 9
move 1 from 8 to 7
move 5 from 4 to 7
move 6 from 7 to 3
move 10 from 9 to 5
move 5 from 3 to 1
move 1 from 1 to 4
move 5 from 1 to 9
move 5 from 1 to 7
move 5 from 4 to 1
move 4 from 1 to 6
move 3 from 1 to 9
move 10 from 5 to 9
move 2 from 7 to 1
move 5 from 3 to 6
move 4 from 5 to 7
move 4 from 2 to 6
move 2 from 5 to 6
move 5 from 2 to 7
move 18 from 6 to 1
move 5 from 9 to 2
move 7 from 9 to 6
move 16 from 1 to 7
move 4 from 6 to 1
move 1 from 2 to 6
move 2 from 2 to 6
move 1 from 2 to 4
move 4 from 9 to 3
move 1 from 2 to 8
move 5 from 7 to 5
move 2 from 9 to 3
move 1 from 5 to 9
move 7 from 3 to 4
move 1 from 9 to 7
move 8 from 1 to 9
move 1 from 8 to 9
move 3 from 6 to 9
move 17 from 7 to 5
move 3 from 4 to 8
move 3 from 4 to 2
move 3 from 8 to 3
move 3 from 3 to 7
move 7 from 9 to 3
move 6 from 5 to 9
move 4 from 9 to 3
move 10 from 7 to 2
move 15 from 5 to 2
move 4 from 6 to 3
move 1 from 3 to 2
move 23 from 2 to 5
move 2 from 4 to 6
move 2 from 6 to 7
move 1 from 7 to 2
move 1 from 6 to 9
move 5 from 9 to 8
move 3 from 8 to 7
move 5 from 2 to 6
move 2 from 2 to 3
move 2 from 6 to 3
move 3 from 6 to 2
move 3 from 6 to 8
move 10 from 5 to 9
move 2 from 7 to 5
move 1 from 5 to 8
move 13 from 9 to 5
move 6 from 5 to 6
move 1 from 6 to 1
move 1 from 7 to 3
move 1 from 7 to 3
move 13 from 5 to 6
move 3 from 3 to 5
move 1 from 2 to 1
move 4 from 8 to 9
move 2 from 2 to 6
move 2 from 5 to 3
move 2 from 3 to 6
move 5 from 6 to 4
move 9 from 5 to 9
move 10 from 6 to 9
move 1 from 1 to 7
move 3 from 3 to 9
move 1 from 8 to 1
move 3 from 6 to 3
move 1 from 7 to 6
move 1 from 8 to 7
move 2 from 6 to 1
move 2 from 6 to 4
move 3 from 4 to 6
move 2 from 1 to 4
move 10 from 9 to 6
move 6 from 4 to 9
move 17 from 9 to 1
move 4 from 9 to 5
move 19 from 1 to 7
move 4 from 5 to 6
move 1 from 9 to 3
move 5 from 3 to 4
move 5 from 4 to 8
move 17 from 6 to 9
move 17 from 9 to 2
move 1 from 6 to 1
move 1 from 1 to 2
move 1 from 8 to 3
move 2 from 3 to 2
move 5 from 7 to 1
move 1 from 7 to 3
move 5 from 2 to 9
move 4 from 8 to 2
move 2 from 7 to 8
move 3 from 9 to 3
move 7 from 3 to 9
move 2 from 8 to 7
move 8 from 2 to 9
move 5 from 9 to 6
move 4 from 3 to 9
move 11 from 2 to 3
move 2 from 6 to 5
move 1 from 9 to 4
move 10 from 7 to 3
move 3 from 1 to 8
move 2 from 6 to 7
move 15 from 3 to 8
move 2 from 3 to 2
move 2 from 1 to 3
move 14 from 9 to 6
move 1 from 4 to 9
move 14 from 6 to 3
move 5 from 7 to 2
move 2 from 9 to 2
move 1 from 5 to 3
move 1 from 5 to 8
move 12 from 3 to 7
move 13 from 7 to 8
move 1 from 6 to 7
move 5 from 2 to 6
move 1 from 6 to 2
move 1 from 7 to 6
move 4 from 6 to 8
move 31 from 8 to 7
move 15 from 7 to 8
move 7 from 7 to 5
move 4 from 2 to 3
move 1 from 6 to 2
move 3 from 5 to 8
move 9 from 7 to 4
move 2 from 2 to 9
move 4 from 5 to 6
move 13 from 3 to 9
move 3 from 3 to 5
move 13 from 9 to 1
move 1 from 3 to 2
move 2 from 6 to 5
move 1 from 3 to 4
move 2 from 6 to 5
move 1 from 9 to 1
move 6 from 8 to 9
move 5 from 5 to 2
move 2 from 9 to 8
move 2 from 1 to 6
move 1 from 9 to 4
move 12 from 8 to 4
move 2 from 6 to 9
move 11 from 4 to 3
move 9 from 4 to 2
move 4 from 9 to 7
move 2 from 5 to 6
move 8 from 3 to 4
move 2 from 3 to 9
move 2 from 8 to 9
move 4 from 4 to 9
move 2 from 6 to 7
move 1 from 3 to 7
move 2 from 9 to 1
move 5 from 4 to 2
move 9 from 1 to 8
move 1 from 4 to 9
move 4 from 9 to 3
move 1 from 3 to 6
move 4 from 8 to 7
move 1 from 3 to 6
move 4 from 1 to 7
move 1 from 3 to 8
move 1 from 1 to 8
move 2 from 6 to 7
move 2 from 9 to 1
move 1 from 4 to 5
move 1 from 1 to 5
move 11 from 8 to 4
move 12 from 2 to 8
move 1 from 9 to 8
move 2 from 4 to 5
move 1 from 1 to 8
move 5 from 2 to 1
move 1 from 3 to 2
move 9 from 7 to 3
move 6 from 7 to 5
move 1 from 3 to 4
move 1 from 5 to 1
move 4 from 2 to 5
move 4 from 4 to 1
move 2 from 7 to 3
move 3 from 4 to 1
move 6 from 3 to 7
move 9 from 8 to 7
move 3 from 8 to 7
move 11 from 5 to 9
move 2 from 4 to 8
move 5 from 8 to 7
move 1 from 9 to 8
move 12 from 9 to 5
move 1 from 4 to 5
move 5 from 1 to 8
move 6 from 8 to 3
move 1 from 3 to 8
move 3 from 7 to 9
move 4 from 7 to 6
move 3 from 1 to 3
move 3 from 1 to 6
move 1 from 8 to 1
move 7 from 6 to 2
move 3 from 1 to 8
move 7 from 3 to 4
move 3 from 4 to 1
move 1 from 4 to 2
move 3 from 1 to 2
move 1 from 7 to 6
move 1 from 8 to 5
move 9 from 5 to 3
move 1 from 6 to 9
move 11 from 3 to 6
move 1 from 4 to 1
move 1 from 3 to 4
move 8 from 6 to 9
move 1 from 3 to 1
move 1 from 9 to 1
move 2 from 6 to 2
move 5 from 5 to 7
move 5 from 9 to 3
move 2 from 8 to 5
move 1 from 1 to 2
move 1 from 9 to 1
move 15 from 7 to 4
move 1 from 1 to 6
move 1 from 6 to 9
move 3 from 9 to 3
move 1 from 3 to 5
move 5 from 5 to 3
move 9 from 2 to 9
move 5 from 4 to 1
move 1 from 6 to 7
move 7 from 9 to 3
move 1 from 4 to 7
move 1 from 9 to 6
move 1 from 6 to 5
move 2 from 1 to 4
move 3 from 9 to 3
move 1 from 5 to 6
move 7 from 4 to 3
move 1 from 9 to 3
move 16 from 3 to 1
move 9 from 1 to 3
move 5 from 4 to 2
move 1 from 6 to 9
move 12 from 1 to 9
move 3 from 2 to 9
move 5 from 7 to 3
move 2 from 4 to 8
move 2 from 7 to 2
move 12 from 3 to 5
move 6 from 2 to 9
move 12 from 3 to 1
move 2 from 8 to 6
move 1 from 6 to 1
move 6 from 5 to 8
move 5 from 3 to 2
move 2 from 5 to 8
move 8 from 1 to 8
move 13 from 9 to 7
move 4 from 7 to 5
move 4 from 1 to 4
move 8 from 5 to 6
move 1 from 1 to 6
move 4 from 7 to 3
move 1 from 3 to 1
move 1 from 1 to 9
move 4 from 9 to 5
move 3 from 3 to 7
move 12 from 8 to 7
move 2 from 4 to 3
move 2 from 6 to 9
move 4 from 8 to 2
move 2 from 3 to 9
move 2 from 4 to 7
move 3 from 5 to 7
move 2 from 9 to 7
move 3 from 6 to 1
move 4 from 6 to 7
move 1 from 5 to 4
move 1 from 9 to 3
move 12 from 2 to 5
move 4 from 9 to 7
move 11 from 5 to 1
move 1 from 6 to 5
move 1 from 1 to 4
move 10 from 1 to 2
move 2 from 5 to 1
move 1 from 3 to 5
move 7 from 2 to 5
move 8 from 7 to 8
move 2 from 2 to 8
move 3 from 9 to 4
move 5 from 4 to 3
move 1 from 5 to 7
move 3 from 7 to 1
move 3 from 5 to 8
move 1 from 2 to 5
move 12 from 7 to 6
move 4 from 1 to 3
move 2 from 5 to 6
move 7 from 3 to 7
move 14 from 6 to 4
move 1 from 5 to 6
move 3 from 1 to 3
move 4 from 3 to 2
move 2 from 5 to 8
move 11 from 7 to 4
move 7 from 4 to 5
move 1 from 3 to 4
move 1 from 5 to 6
move 14 from 8 to 7
move 11 from 7 to 3
move 2 from 2 to 6
move 1 from 2 to 3
move 5 from 5 to 4
move 4 from 6 to 4
move 8 from 7 to 8
move 3 from 7 to 3
move 1 from 2 to 1
move 5 from 8 to 2
move 4 from 4 to 3
move 1 from 2 to 9
move 1 from 1 to 9
move 3 from 2 to 1
move 1 from 5 to 4
move 3 from 8 to 1
move 1 from 7 to 4
move 4 from 3 to 9
move 1 from 8 to 7
move 2 from 9 to 1
move 6 from 3 to 4
move 28 from 4 to 7
move 15 from 7 to 8
move 3 from 3 to 8
move 1 from 2 to 9
move 2 from 3 to 2
move 7 from 1 to 4
move 10 from 4 to 5
move 10 from 5 to 6
move 3 from 8 to 2
move 1 from 1 to 7
move 1 from 4 to 7
move 1 from 9 to 6
move 9 from 6 to 7
move 1 from 2 to 4
move 1 from 9 to 5";

        public override void Part1()
        {
            var stacks = GetStacks();
            var lines = input.Split("\r\n");
            for (int i = 0; i < lines.Length; i++)
            {
                var (count, from, to) = Parse(lines[i]);
                for (int j = 0; j < count; j++)
                {
                    var cnt = stacks[from - 1].Count;
                    var item = stacks[from - 1][cnt - 1];
                    stacks[from - 1].RemoveAt(cnt - 1);
                    stacks[to - 1].Add(item);
                }
            }

            Answer(stacks.Aggregate("", (r, s) => r + s.Last()));
        }

        public override void Part2()
        {
            var stacks = GetStacks();
            var lines = input.Split("\r\n");
            for (int i = 0; i < lines.Length; i++)
            {
                var (count, from, to) = Parse(lines[i]);
                var cnt = stacks[from - 1].Count;
                var items = stacks[from - 1].GetRange(cnt-count, count);
                stacks[from - 1].RemoveRange(cnt - count, count);
                stacks[to - 1].AddRange(items);
            }

            Answer(stacks.Aggregate("", (r, s) => r + s.Last()));
        }

        private List<char>[] GetStacks()
        {
            var lines = stacks.Split("\r\n");
            var result = new List<List<char>>();
            for (int i = 1; i < lines.Last().Length; i += 4)
                result.Add(new List<char>());

            for (int i = lines.Length - 1; i >= 0; i--)
                for (int j = 0; j < lines[i].Length; j += 4)
                {
                    var c = lines[i][j + 1];
                    if (c != ' ')
                        result[j / 4].Add(lines[i][j + 1]); 
                }

            return result.ToArray();
        }

        private (int count, int from, int to) Parse(string line)
        {
            var parts = line.Split(' ');
            return (int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]));
        }
    }
}
