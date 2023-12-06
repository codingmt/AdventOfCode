using System.Text.RegularExpressions;

namespace AOC._2023
{
    internal class Day05 : DayBase
    {
        public override void Part1()
        {
            using var reader = GetInputReader();
            var seeds = reader.ReadLine()[6..]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(uint.Parse)
                .ToArray();
            reader.ReadLine();
            var maps = Map.ReadMaps(reader).ToList();

            var map = ChainMappingsForward(maps);

            Answer(seeds.Min(map));
        }

        public override void Part2()
        {
            using var reader = GetInputReader();
            var seeds = reader.ReadLine()[6..]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(uint.Parse)
                .ToArray();
            reader.ReadLine();
            var maps = Map.ReadMaps(reader).ToList();

            Func<uint, uint> map = ChainMappingsBackward(maps);

            var lastMapRange = maps.Last().Ranges.Last();
            for (uint i = 1; true; i++)
            {
                var input = map(i);
                for (int j = 0; j < seeds.Length - 1; j += 2)
                    if (input >= seeds[j] && input < seeds[j] + seeds[j + 1])
                    {
                        Answer(i);
                        return;
                    }
            }
        }

        private static Func<uint, uint> ChainMappingsForward(List<Map> maps)
        {
            Func<uint, uint> map = maps[0].MapForward;
            for (int i = 1; i < maps.Count; i++)
            {
                var m = map;
                Func<uint, uint> mapper = maps[i].MapForward;
                map = input => mapper(m(input));
            }

            return map;
        }

        private static Func<uint, uint> ChainMappingsBackward(List<Map> maps)
        {
            Func<uint, uint> map = maps.Last().MapBackward;
            for (int i = maps.Count - 2; i >= 0; i--)
            {
                var m = map;
                Func<uint, uint> mapper = maps[i].MapBackward;
                map = input => mapper(m(input));
            }

            return map;
        }

        private class Map
        {
            public readonly string From;
            public readonly string To;

            public readonly List<Range> Ranges = new();

            public Map(TextReader reader)
            {
                var header = reader.ReadLine();
                var headerMatch = Regex.Match(header, @"^(?<from>\w+)\-to\-(?<to>\w+) map\:$");
                if (!headerMatch.Success)
                    throw new Exception($"Header mismatch");

                From = headerMatch.Groups["from"].Value;
                To = headerMatch.Groups["to"].Value;

                while (reader.Peek() != -1)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;

                    var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 3)
                        throw new Exception("Expected 3 parts in the range");

                    Ranges.Add(new Range(uint.Parse(parts[1]), uint.Parse(parts[0]), uint.Parse(parts[2])));
                }
            }

            public uint MapForward(uint input)
            {
                var range = Ranges.FirstOrDefault(r => r.From <= input && input - r.From < r.Length);
                return range == null
                    ? input
                    : range.To + (input - range.From);
            }

            public uint MapBackward(uint output)
            {
                var range = Ranges.FirstOrDefault(r => r.To <= output && output - r.To < r.Length);
                return range == null
                    ? output
                    : range.From + (output - range.To);
            }

            public static IEnumerable<Map> ReadMaps(TextReader reader)
            {
                while (reader.Peek() != -1)
                {
                    yield return new Map(reader);
                }
            }

            public class Range
            {
                public readonly uint From;
                public readonly uint To;
                public readonly uint Length;

                public Range(uint from, uint to, uint length)
                {
                    From = from;
                    To = to;
                    Length = length;
                }
            }
        }
    }
}
