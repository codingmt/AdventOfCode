namespace AOC
{
    internal class Day13 : DayBase
    {
        public override void Part1()
        {
            var lines = GetInputLines();
            var correct = 0;
            var index = 1;
            for (int line = 0; line < lines.Length; line += 3, index++)
            {
                var package1 = ArrayItem.ParseArray(lines[line]);
                var package2 = ArrayItem.ParseArray(lines[line + 1]);
                if (package1.CompareTo(package2) == -1) 
                    correct += index;
            }
            Answer(correct);
        }

        public override void Part2()
        {
            var lines = GetInput().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var packages = lines.Select(line => ArrayItem.ParseArray(line)).ToList();
            var devider1 = ArrayItem.ParseArray("[[2]]");
            var devider2 = ArrayItem.ParseArray("[[6]]");
            packages.Add(devider1);
            packages.Add(devider2);
            packages.Sort();
            Answer((packages.IndexOf(devider1) + 1) * (packages.IndexOf(devider2) + 1));
        }

        private abstract class ItemBase : IComparable<ItemBase>
        {
            public abstract int CompareTo(ItemBase other);
        }

        private class NumberItem : ItemBase
        {
            public int Value;

            public NumberItem(int value)
            {
                Value = value;
            }

            public NumberItem(string content, ref int pos)
            {
                var len = 1;
                while (pos + len < content.Length && content[pos + len] >= '0' && content[pos + len] <= '9')
                    len++;
                Value = int.Parse(content.Substring(pos, len));
                pos += len;
            }

            public override int CompareTo(ItemBase other)
            {
                if (other is NumberItem n)
                    return Value.CompareTo(n.Value);

                if (other is ArrayItem a)
                    return new ArrayItem(Value).CompareTo(a);

                throw new NotImplementedException();
            }

            public override string ToString() => Value.ToString();
        }

        private class ArrayItem : ItemBase
        {
            private List<ItemBase> items = new List<ItemBase>();

            public ArrayItem(params int[] numbers)
            {
                items.AddRange(numbers.Select(x => new NumberItem(x)));
            }

            public ArrayItem(string content, ref int pos)
            {
                while (pos < content.Length)
                    switch (content[pos])
                    {
                        case >= '0' and <= '9':
                            items.Add(new NumberItem(content, ref pos));
                            break;
                        case '[':
                            pos++;
                            items.Add(new ArrayItem(content, ref pos));
                            break;
                        case ' ' or ',':
                            pos++;
                            break;
                        case ']':
                            pos++;
                            return;
                        default:
                            throw new Exception($"Unexpected character on position {pos}.");
                    }
            }

            public static ArrayItem ParseArray(string content)
            {
                var pos = 1;
                return new ArrayItem(content, ref pos);
            }

            public override int CompareTo(ItemBase other)
            {
                if (other is NumberItem n)
                    return -n.CompareTo(this);

                if (other is ArrayItem a)
                {
                    var count = items.Count < a.items.Count ? items.Count : a.items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var itemComparison = items[i].CompareTo(a.items[i]);
                        if (itemComparison != 0)
                            return itemComparison;
                    }

                    return items.Count < a.items.Count ? -1 : items.Count == a.items.Count ? 0 : 1;
                }

                throw new NotImplementedException();
            }

            public override string ToString() => $"[{string.Join(',',items)}]";
        }
    }
}
