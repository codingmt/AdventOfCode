namespace AOC
{
    internal class Day16 : DayBase
    {
        public override void Part1()
        {
            var valves = GetInputLines("test")
                .Select(x => new Valve(x))
                .ToDictionary(x => x.Name);

            // create small paths of valves to prioritize valves
            var flowRates = valves.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value.FlowRate);
            var subpaths = new List<(string[] valves, Func<int> getFlowRate)>();
            var subpathdepth = 10;
            foreach (var valve in valves.Keys)
                addSubPaths(new List<string> { valve });

            var flow = 0;
            var path = new List<string> { "AA" };
            var minutes = 30;
            while (minutes > 0)
            {
                var nextvalve = subpaths
                    .Where(x => x.valves[0] == path.Last())
                    .Select(x => new { NextValve = x.valves[1], flowRate = x.getFlowRate() })
                    .OrderByDescending(x => x.flowRate)
                    .FirstOrDefault();
                if (nextvalve.flowRate == 0)
                    break;
                minutes--;
                // Open the valve? Guess it's importance
                if (nextvalve.flowRate * .02 < flowRates[nextvalve.NextValve])
                {
                    minutes--;
                    flow += minutes * flowRates[nextvalve.NextValve];
                    flowRates[nextvalve.NextValve] = 0;
                }
                path.Add(nextvalve.NextValve);
            }

            Answer(flow);

            void addSubPaths(List<string> subpath)
            { 
                if (subpath.Count == subpathdepth)
                {
                    var a = subpath.ToList();
                    subpaths.Add((
                        a.ToArray(), 
                        () => a
                            .Select((v, i) => 
                                (subpathdepth - i) * (a.IndexOf(v) < i ? 0 : flowRates[v]))
                            .Sum()
                        ));
                }
                else
                {
                    var valve = valves[subpath.Last()];
                    foreach (var item in valve.ConnectedTo)
                    {
                        subpath.Add(item);
                        addSubPaths(subpath);
                        subpath.RemoveAt(subpath.Count - 1);
                    }
                }
            }
        }

        public override void Part2()
        {
            var valves = GetInputLines("test")
                .Select(x => new Valve(x))
                .ToDictionary(x => x.Name);

            // create small paths of valves to prioritize valves
            var flowRates = valves.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value.FlowRate);
            var subpaths = new List<(string[] valves, Func<int> getFlowRate)>();
            var subpathdepth = 10;
            foreach (var valve in valves.Keys)
                addSubPaths(new List<string> { valve });

            var flow = 0;
            var path1 = new List<string> { "AA" };
            var path2 = new List<string> { "AA" };
            var minutes1 = 26;
            var minutes2 = 26;
            while (minutes1 > 0 || minutes2 > 0)
            {
                var nextvalve1 = subpaths
                    .Where(x => x.valves[0] == path1.Last())
                    .Select(x => new { NextValve = x.valves[1], flowRate = x.getFlowRate() })
                    .OrderByDescending(x => x.flowRate)
                    .FirstOrDefault();
                var nextvalve2 = subpaths
                    .Where(x => x.valves[0] == path1.Last() && x.valves[1] != nextvalve1.NextValve)
                    .Select(x => new { NextValve = x.valves[1], flowRate = x.getFlowRate() })
                    .OrderByDescending(x => x.flowRate)
                    .FirstOrDefault();
                if (nextvalve1.flowRate == 0)
                    break;
                if (minutes1 > 1)
                {
                    minutes1--;
                    // Open the valve? Guess it's importance
                    if (nextvalve1.flowRate * .02 < flowRates[nextvalve1.NextValve])
                    {
                        minutes1--;
                        flow += minutes1 * flowRates[nextvalve1.NextValve];
                        flowRates[nextvalve1.NextValve] = 0;
                    }
                    path1.Add(nextvalve1.NextValve);
                }
                if (minutes2 > 1)
                {
                    minutes2--;
                    // Open the valve? Guess it's importance
                    if (nextvalve2.flowRate * .02 < flowRates[nextvalve2.NextValve])
                    {
                        minutes2--;
                        flow += minutes2 * flowRates[nextvalve2.NextValve];
                        flowRates[nextvalve2.NextValve] = 0;
                    }
                    path2.Add(nextvalve2.NextValve);
                }
            }

            Answer(flow);

            void addSubPaths(List<string> subpath)
            {
                if (subpath.Count == subpathdepth)
                {
                    var a = subpath.ToList();
                    subpaths.Add((
                        a.ToArray(),
                        () => a
                            .Select((v, i) =>
                                (subpathdepth - i) * (a.IndexOf(v) < i ? 0 : flowRates[v]))
                            .Sum()
                        ));
                }
                else
                {
                    var valve = valves[subpath.Last()];
                    foreach (var item in valve.ConnectedTo)
                    {
                        subpath.Add(item);
                        addSubPaths(subpath);
                        subpath.RemoveAt(subpath.Count - 1);
                    }
                }
            }
        }

        private class Valve
        {
            public string Name;
            public int FlowRate;
            public string[] ConnectedTo;
            public bool Opened;

            public Valve(string line)
            {
                var parts = line.Split(new[] { "Valve ", " has flow rate=", "; tunnels lead to valves ", "; tunnel leads to valve " }, StringSplitOptions.RemoveEmptyEntries);
                Name = parts[0];
                FlowRate = int.Parse(parts[1]);
                ConnectedTo = parts[2].Split(", ");
            }

            public override string ToString() => Name;
        }
    }
}
