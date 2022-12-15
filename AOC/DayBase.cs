namespace AOC
{
    internal abstract class DayBase
    {
        public abstract void Part1();

        public abstract void Part2();

        protected string GetInput(string filename = null) => File.ReadAllText($"input\\{GetType().Name}{filename}.txt");

        protected string[] GetInputLines(string filename = null) => GetInput(filename).Split("\r\n");

        protected void Answer(object answer)
        {
            Clipboard.SetText(answer.ToString());
            Console.WriteLine("Answer sent to clipboard: " + answer);
        }
    }
}
