namespace AOC
{
    internal abstract class DayBase
    {
        public abstract void Run1();

        public abstract void Run2();

        protected void Answer(object answer)
        {
            Clipboard.SetText(answer.ToString());
            Console.WriteLine("Answer sent to clipboard: " + answer);
        }
    }
}
