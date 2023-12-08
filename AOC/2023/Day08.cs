namespace AOC._2023
{
    internal class Day08 : DayBase
    {
        private Dictionary<string, (string l, string r)> _nodes;
        private string _lrInstr;

        public override void Part1()
        {
            ReadFile();

            var steps = 0;
            var node = "AAA";
            do
            {
                for (var i = 0; i < _lrInstr.Length; i++)
                {
                    node = _lrInstr[i] == 'L' ? _nodes[node].l : _nodes[node].r;
                    steps++;
                }
            } while (node != "ZZZ");

            Answer(steps);
        }

        public override void Part2()
        {
            // Get the result per execution of the lr instruction, per node
            var resultPerNode = _nodes.ToDictionary(x => x.Key, x => x.Key);
            long lrInstrLength = _lrInstr.Length;
            for (var i = 0; i < lrInstrLength; i++)
            {
                var left = _lrInstr[i] == 'L';
                foreach (var key in resultPerNode.Keys)
                    resultPerNode[key] = left 
                        ? _nodes[resultPerNode[key]].l 
                        : _nodes[resultPerNode[key]].r;
            }

            // Get the number of steps to the next Z-ending node, per node
            var stepsToZPerNode = _nodes.ToDictionary(x => x.Key, x => (node: x.Key, steps: 0L));
            long steps = 0;
            do
            {
                steps += lrInstrLength;
                foreach (var key in stepsToZPerNode.Where(x => x.Value.steps == 0L).Select(x => x.Key))
                {
                    var node = resultPerNode[stepsToZPerNode[key].node];
                    stepsToZPerNode[key] = 
                        node == stepsToZPerNode[key].node
                        ? (node, -1) // points to itself, disable
                        : node.EndsWith('Z')
                            ? (node, steps)
                            : (node, 0);
                }
            } while (stepsToZPerNode.Values.Any(x => x.steps == 0));

            // Now move to the next Z node until all step counters are equal
            steps = 1; // preset to 1 instead of 0 to get started
            var paths = _nodes.Keys.Where(k => k.EndsWith('A')).Select(n => (node: n, steps: 0L)).ToArray();
            do
            {
                for (var p = 0; p < paths.Length; p++)
                    if (paths[p].steps < steps)
                    {
                        var stepsToZ = stepsToZPerNode[paths[p].node];
                        paths[p] = (stepsToZ.node, steps: paths[p].steps + stepsToZ.steps);
                        if (paths[p].steps > steps)
                            steps = paths[p].steps;
                    }
            } while (!paths.All(p => p.steps == steps));

            Answer(steps);
        }

        private void ReadFile(string filename = null)
        {
            var reader = GetInputReader(filename);
            _lrInstr = reader.ReadLine();
            reader.ReadLine();

            _nodes = new Dictionary<string, (string l, string r)>();
            while (true)
            {
                var line = reader.ReadLine();
                if (line == null)
                    break;

                var parts = line.Split(new[] { " = (", ", ", ")" }, StringSplitOptions.RemoveEmptyEntries);
                _nodes.Add(parts[0], (parts[1], parts[2]));
            }
        }
    }
}
