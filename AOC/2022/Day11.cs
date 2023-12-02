using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AOC._2022;

internal class Day11 : DayBase
{
    public override void Part1()
    {
        var monkeys = Monkey.Parse(GetInput());

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

    public override void Part2()
    {
        var monkeys = Monkey.Parse(GetInput());
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
