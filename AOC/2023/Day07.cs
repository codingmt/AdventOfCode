namespace AOC._2023
{
    internal class Day07 : DayBase
    {
        public override void Part1()
        {
            var hands = GetInputLines()
                .Select(l => l.Split(" "))
                .Select(parts => new Hand1(parts[0], int.Parse(parts[1])))
                .ToArray();
            Array.Sort(hands);
            var score = hands
                .Select((h, i) => (i + 1) * h.Bid)
                .Sum();
            Answer(score);
        }

        public override void Part2()
        {
            var hands = GetInputLines()
                .Select(l => l.Split(" "))
                .Select(parts => new Hand2(parts[0], int.Parse(parts[1])))
                .ToArray();
            Array.Sort(hands);
            var score = hands
                .Select((h, i) => (i + 1) * h.Bid)
                .Sum();
            Answer(score);
        }

        private class Hand2 : Hand1
        {
            protected override int Jvalue => 1;

            public Hand2(string hand, int bid) : base(hand, bid)
            { }

            protected override HandType GetHandType()
            {
                var cards = Hand.ToCharArray();
                Array.Sort(cards);

                int equals = 1, jcards = 0;
                int otherpairs = 0, othertriples = 0, otherquartets = 0;

                var isJ = cards[0] == 'J';
                if (isJ) jcards++;

                for (int i = 1; i < cards.Length; i++)
                {
                    if (cards[i] == cards[i - 1])
                    {
                        equals++;
                        if (isJ) jcards++;
                    }
                    else
                    {
                        if (!isJ)
                        {
                            if (equals == 2)
                                otherpairs++;
                            else if (equals == 3)
                                othertriples++;
                            else if (equals == 4)
                                otherquartets++;
                        }

                        equals = 1;
                        isJ = cards[i] == 'J';
                        if (isJ) jcards++;
                    }
                }

                if (!isJ)
                {
                    if (equals == 2)
                        otherpairs++;
                    else if (equals == 3)
                        othertriples++;
                    else if (equals == 4)
                        otherquartets++;
                }

                return equals == 5 || jcards == 4 ? HandType.FiveOfAKind
                    : jcards == 3
                        ? (otherpairs == 1? HandType.FiveOfAKind : HandType.FourOfAKind)
                    : jcards == 2
                        ? (othertriples == 1 ? HandType.FiveOfAKind 
                            : otherpairs == 1 ? HandType.FourOfAKind
                            : HandType.ThreeOfAKind)
                    : jcards == 1
                        ? (otherquartets == 1 ? HandType.FiveOfAKind
                            : othertriples == 1 ? HandType.FourOfAKind
                            : otherpairs == 2 ? HandType.FullHouse
                            : otherpairs == 1 ? HandType.ThreeOfAKind
                            : HandType.OnePair)
                    : otherquartets == 1 ? HandType.FourOfAKind
                    : othertriples == 1
                        ? (otherpairs == 1 ? HandType.FullHouse : HandType.ThreeOfAKind)
                    : otherpairs == 2 ? HandType.TwoPair
                    : otherpairs == 1 ? HandType.OnePair
                    : HandType.HighCard;
            }
        }

        private class Hand1 : IComparable<Hand1>
        {
            public enum HandType
            {
                HighCard,
                OnePair,
                TwoPair,
                ThreeOfAKind,
                FullHouse,
                FourOfAKind,
                FiveOfAKind
            }

            protected virtual int Jvalue { get; } = 11;

            protected readonly string Hand;

            public readonly int Bid;
            public readonly long Value;
            public readonly HandType HType;

            public Hand1(string hand, int bid)
            {
                Hand = hand;
                Bid = bid;
                Value = CalcValue();
                HType = GetHandType();
            }

            private long CalcValue()
            {
                var result = 0L;
                for (int i = 0; i < Hand.Length; i++)
                {
                    result <<= 4;
                    result +=
                        Hand[i] switch
                        {
                            'A' => 14,
                            'K' => 13,
                            'Q' => 12,
                            'J' => Jvalue,
                            'T' => 10,
                            >='2' => Hand[i] - '0',
                            _ => 0
                        };
                }

                return result;
            }

            protected virtual HandType GetHandType()
            {
                var cards = Hand.ToCharArray();
                Array.Sort(cards);

                int equals = 1, pairs = 0, triples = 0;
                for (int i = 1; i < cards.Length; i++)
                {
                    if (cards[i] == cards[i-1])
                        equals++;
                    else
                    {
                        if (equals == 2)
                            pairs++;
                        else if (equals == 3)
                            triples++;
                        else if (equals == 4)
                            break;
                        equals = 1;
                    }
                }

                return equals == 5 ? HandType.FiveOfAKind
                    : equals == 4 ? HandType.FourOfAKind
                    : equals == 3
                        ? (pairs == 1 ? HandType.FullHouse : HandType.ThreeOfAKind)
                    : equals == 2
                        ? (pairs == 1 ? HandType.TwoPair : triples == 1 ? HandType.FullHouse : HandType.OnePair)
                    : triples == 1 ? HandType.ThreeOfAKind
                    : pairs == 2 ? HandType.TwoPair
                    : pairs == 1 ? HandType.OnePair
                    : HandType.HighCard;
            }

            public int CompareTo(Hand1 other) => 
                HType > other.HType ? 1 
                : HType < other.HType ? -1
                : Value > other.Value ? 1
                : Value < other.Value ? -1
                : 0;

            public override string ToString() => Hand;
        }
    }
}
