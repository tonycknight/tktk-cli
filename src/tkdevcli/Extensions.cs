namespace tkdevcli
{
    internal static class Extensions
    {
        public static int ApplyDefault(this int value, Func<int, bool> applyDefault, int defaultValue) 
            => applyDefault(value) ? defaultValue : value;

        public static IEnumerable<string> ToTable(this IEnumerable<(string, string)> keyValues)
        {
            var maxHeader = keyValues.Max(t => t.Item1.Length);
            Func<string, string> fmtHeader = hdr => $"{hdr}:".PadRight(maxHeader, ' ');

            return keyValues.Select(t => $"{fmtHeader(t.Item1)}\t{t.Item2}");
        }
    }
}
