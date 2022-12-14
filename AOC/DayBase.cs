namespace AOC
{
    internal abstract class DayBase
    {
        public abstract void Part1();

        public abstract void Part2();

        protected void Answer(object answer)
        {
            Clipboard.SetText(answer.ToString());
            Console.WriteLine("Answer sent to clipboard: " + answer);
        }
    }
}
