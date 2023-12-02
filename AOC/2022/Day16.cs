namespace AOC._2022;

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
        var subpaths = new List<((string name, bool open)[] valves, Func<int> getFlowRate)>();
        var subpathdepth = 8;
        foreach (var valve in valves.Keys)
        {
            addSubPaths(new List<(string valve, bool open)> { (valve, true) });
            addSubPaths(new List<(string valve, bool open)> { (valve, false) });
        }

        var currentflow = 0;
        var totalflow = 0;
        var path1 = new List<(string name, bool open)> { ("AA", false) };
        var path2 = new List<(string name, bool open)> { ("AA", false) };
        var minutes = 26;
        while (minutes > 0)
        {
            var nextvalve1 = subpaths
                .Where(x => x.valves[0].name == path1.Last().name)
                .Select(x => new { NextValve = x.valves[1], OpenCurrent = x.valves[0].open, flowRate = x.getFlowRate() })
                .OrderByDescending(x => x.flowRate)
                .FirstOrDefault();

            if (nextvalve1.OpenCurrent && flowRates[path1.Last().name] != 0)
            {
                totalflow += flowRates[path1.Last().name];
                flowRates[path1.Last().name] = 0;
            }
            else
            {
                path1.Add(nextvalve1.NextValve);
            }

            var nextvalve2 = subpaths
                .Where(x => x.valves[0].name == path2.Last().name && x.valves[1] != nextvalve1.NextValve)
                .Select(x => new { NextValve = x.valves[1], OpenCurrent = x.valves[0].open, flowRate = x.getFlowRate() })
                .OrderByDescending(x => x.flowRate)
                .FirstOrDefault();
            if (nextvalve2.OpenCurrent && flowRates[path2.Last().name] != 0)
            {
                totalflow += flowRates[path2.Last().name];
                flowRates[path2.Last().name] = 0;
            }
            else
            {
                path2.Add(nextvalve2.NextValve);
            }

            totalflow += currentflow;
        }

        Answer(totalflow);

        void addSubPaths(List<(string valve, bool open)> subpath)
        {
            if (subpath.Count >= subpathdepth)
            {
                var a = subpath.ToList();
                subpaths.Add((
                    a.ToArray(),
                    () => 
                    {
                        var depth = subpathdepth * 2;
                        var flow = 0;
                        var result = 0;
                        foreach ( var v in a )
                        {
                            result += flow * depth;
                            depth--;
                            if (v.open)
                            {
                                flow += flowRates[v.valve];
                                result += flow * depth;
                                depth--;
                            }
                        }
                        return result;
                    }));
            }
            else
            {
                var valve = valves[subpath.Last().valve];
                foreach (var item in valve.ConnectedTo)
                {
                    subpath.Add((item, false));
                    addSubPaths(subpath);
                    subpath.RemoveAt(subpath.Count - 1);

                    if (!subpath.Any(x => x.valve == valve.Name && x.open))
                    {
                        subpath.Add((item, true));
                        addSubPaths(subpath);
                        subpath.RemoveAt(subpath.Count - 1);
                    }
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
