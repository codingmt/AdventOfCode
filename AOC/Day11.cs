using System.Configuration;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AOC
{
    internal class Day11 : DayBase
    {
        public string testinput = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

        private string input = @"Monkey 0:
  Starting items: 96, 60, 68, 91, 83, 57, 85
  Operation: new = old * 2
  Test: divisible by 17
    If true: throw to monkey 2
    If false: throw to monkey 5

Monkey 1:
  Starting items: 75, 78, 68, 81, 73, 99
  Operation: new = old + 3
  Test: divisible by 13
    If true: throw to monkey 7
    If false: throw to monkey 4

Monkey 2:
  Starting items: 69, 86, 67, 55, 96, 69, 94, 85
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 6
    If false: throw to monkey 5

Monkey 3:
  Starting items: 88, 75, 74, 98, 80
  Operation: new = old + 5
  Test: divisible by 7
    If true: throw to monkey 7
    If false: throw to monkey 1

Monkey 4:
  Starting items: 82
  Operation: new = old + 8
  Test: divisible by 11
    If true: throw to monkey 0
    If false: throw to monkey 2

Monkey 5:
  Starting items: 72, 92, 92
  Operation: new = old * 5
  Test: divisible by 3
    If true: throw to monkey 6
    If false: throw to monkey 3

Monkey 6:
  Starting items: 74, 61
  Operation: new = old * old
  Test: divisible by 2
    If true: throw to monkey 3
    If false: throw to monkey 1

Monkey 7:
  Starting items: 76, 86, 83, 55
  Operation: new = old + 4
  Test: divisible by 5
    If true: throw to monkey 4
    If false: throw to monkey 0";

        public override void Run1()
        {
            var monkeys = Monkey.Parse(input);

            for (int round = 0; round < 20; round++)
            {
                foreach (var monkey in monkeys)
                {
                    foreach (var item in monkey.Items)
                    {
                        monkey.Inspections++;
                        var newValue = (int)Math.Floor(monkey.Operation(item) / 3f);
                        var test = newValue % monkey.TestValue == 0;
                        monkeys[test ? monkey.TargetIfTrue : monkey.TargetIfFalse].Items.Add(newValue);
                    }
                    monkey.Items.Clear();
                }
            }

            var topMonkeys = monkeys.OrderByDescending(x => x.Inspections).ToArray();
            Answer(topMonkeys[0].Inspections * topMonkeys[1].Inspections);
        }

        public override void Run2()
        {
            var monkeys = Monkey.Parse(input);
            var modulo = monkeys.Aggregate(1L, (v, m) => v * m.TestValue);

            for (int round = 0; round < 10_000; round++)
            {
                foreach (var monkey in monkeys)
                {
                    foreach (var item in monkey.Items)
                    {
                        var newValue = monkey.Operation(item) % modulo;
                        var test = newValue % monkey.TestValue == 0;
                        monkeys[test ? monkey.TargetIfTrue : monkey.TargetIfFalse].Items.Add(newValue);
                    }

                    monkey.Inspections += monkey.Items.Count;
                    monkey.Items.Clear();
                }
            }

            var topMonkeys = monkeys.OrderByDescending(x => x.Inspections).ToArray();
            Answer(topMonkeys[0].Inspections * topMonkeys[1].Inspections);
        }

        private class Monkey
        {
            public int Number;
            public long Inspections;
            public List<long> Items = new List<long>();
            public Func<long, long> Operation;
            public int TestValue;
            public int TargetIfTrue;
            public int TargetIfFalse;

            public static Monkey[] Parse(string input) => 
                Regex.Matches(input, @"Monkey (?<mn>\d+):\s*Starting items: (?<items>\d+(, \d+)*)\s*Operation: new = old (?<operator>.) (?<operand>\S+)\s*Test: divisible by (?<test>\d+)\s*If true: throw to monkey (?<targetTrue>\d+)\s*If false: throw to monkey (?<targetFalse>\d+)", RegexOptions.Multiline)
                        .Cast<Match>()
                        .Select(m =>
                            new Monkey
                            {
                                Number = int.Parse(m.Groups["mn"].Value),
                                Items = m.Groups["items"].Value.Split(',').Select(x => long.Parse(x)).ToList(),
                                Operation = ParseOperation(m.Groups["operator"].Value, m.Groups["operand"].Value),
                                TestValue = int.Parse(m.Groups["test"].Value),
                                TargetIfTrue = int.Parse(m.Groups["targetTrue"].Value),
                                TargetIfFalse = int.Parse(m.Groups["targetFalse"].Value)
                            })
                        .ToArray();

            private static Func<long, long> ParseOperation(string op, string operand)
            {
                var arg = Expression.Parameter(typeof(long));
                var operand1 = operand == "old" ? (Expression)arg : Expression.Constant(long.Parse(operand));
                Expression body;

                switch (op)
                {
                    case "*": body = Expression.Multiply(arg, operand1); break;
                    case "+": body = Expression.Add(arg, operand1); break;
                    default: throw new ArgumentException($"Unsuppored operand: {op}");
                }

                return Expression.Lambda<Func<long, long>>(body, arg).Compile();
            }
        }
    }
}
