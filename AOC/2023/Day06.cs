using System.Text.RegularExpressions;

namespace AOC._2023
{
    internal class Day06 : DayBase
    {
        public override void Part1()
        {
            var races = Race.GetRaces(GetInputLines());
            var waysToWin = races
                .Select(GetWaysToWin)
                .ToArray();
            Answer(waysToWin.Aggregate(1L, (r, x) => r * x));
        }

        public override void Part2()
        {
            var lines = GetInputLines();
            var race =
                new Race
                {
                    Time = long.Parse(Regex.Replace(lines[0], @"\D", string.Empty)),
                    Distance = long.Parse(Regex.Replace(lines[1], @"\D", string.Empty))
                };
            Answer(GetWaysToWin(race));
        }

        private static long GetWaysToWin(Race r)
        {
            /*
            distance = traveling time * speed
            traveling time = time - holding time
            speed = holding time
            distance = (time - holding time) * holding time
            distance = time * holding time - holding time ^ 2
            time * holding time - holding time ^ 2 > distance to beat
            time * holding time - holding time ^ 2 - distance to beat > 0
            holding time ^ 2 - time * holding time + distance to beat < 0
            ABC formula
            https://www.slimleren.nl/onderwerpen/wiskunde/12.227/basis-de-abc-formule
            a = 1, b = -time, c = distance to beat
            D = time ^ 2 - 4 * distance to beat
            We go further between holding times:
            (time - SQRT(D)) / 2 and (time + SQRT(D)) / 2
             */

            var d = Math.Pow(r.Time, 2) - 4 * r.Distance;
            
            var fromValue = (r.Time - Math.Sqrt(d)) / 2;
            var from = (long)Math.Ceiling(fromValue);
            if (fromValue == Math.Round(fromValue)) // will match the record instead of breaking it
                from++;

            var toValue = (r.Time + Math.Sqrt(d)) / 2;
            var to = (long)Math.Floor(toValue);
            if (toValue == Math.Round(toValue)) // will match the record instead of breaking it
                to--;

            return to - from + 1;
        }

        private class Race
        {
            public long Time;
            public long Distance;

            public static Race[] GetRaces(string[] lines)
            {
                var times = Regex.Matches(lines[0], @"\d+");
                var distances = Regex.Matches(lines[1], @"\d+");

                return times
                    .Select(
                        (m, i) => 
                        new Race 
                        { 
                            Time = long.Parse(m.Value), 
                            Distance = long.Parse(distances[i].Value) 
                        })
                    .ToArray();
            }
        }
    }
}
