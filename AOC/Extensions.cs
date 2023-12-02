namespace AOC
{
    internal static class Extensions
    {
        public static string Right(this string s, int characters) =>
            s == null || s.Length < characters ? s : s.Substring(s.Length - characters);
    }
}
