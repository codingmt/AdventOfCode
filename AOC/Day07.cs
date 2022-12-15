namespace AOC
{
    internal class Day07 : DayBase
    {
        public override void Part1()
        {
            var fs = ParseInput();
            Answer(fs.GetAllDirectories().Where(x => x.GetSize() < 100_000).Sum(x => x.GetSize()));
        }

        public override void Part2()
        {
            const int totalSpace = 70_000_000;
            const int neededSpace = 30_000_000;
            var fs = ParseInput();
            var currentlyAvailable = totalSpace - fs.GetSize();
            var freeUpSpace = neededSpace - currentlyAvailable;
            Answer(fs.GetAllDirectories().Where(x => x.GetSize() >= freeUpSpace).OrderBy(x => x.GetSize()).First().GetSize());
        }

        private Dir ParseInput()
        {
            var root = new Dir();
            var currentDir = root;

            var lines = GetInputLines();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "$ cd /")
                    currentDir = root;
                else if (lines[i].StartsWith("$ cd .."))
                    currentDir = currentDir.Parent ?? root;
                else if (lines[i].StartsWith("$ cd "))
                    currentDir = currentDir.Entries.OfType<Dir>().First(x => x.Name == lines[i].Substring(5));
                else if (lines[i].StartsWith("dir "))
                    currentDir.Entries.Add(new Dir { Name = lines[i].Substring(4), Parent = currentDir });
                else if (!lines[i].StartsWith("$ "))
                    currentDir.Entries.Add(new File { Name = lines[i].Split(' ')[1], Size = int.Parse(lines[i].Split(' ')[0]) });
            }

            return root;
        }

        internal abstract class Entry
        {
            public string Name;
            public abstract int GetSize();
        }

        internal class Dir : Entry
        {
            public Dir Parent = null;

            public List<Entry> Entries = new List<Entry>();

            public override int GetSize() => Entries.Sum(x => x.GetSize());

            public IEnumerable<Dir> GetAllDirectories()
            {
                foreach (var dir in Entries.OfType<Dir>())
                {
                    yield return dir;
                    foreach (var subdir in dir.GetAllDirectories())
                        yield return subdir;
                }
            }
        }

        internal class File : Entry
        {
            public int Size;

            public override int GetSize() => Size;
        }
    }
}
